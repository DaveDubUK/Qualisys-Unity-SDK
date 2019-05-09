// Unity SDK for Qualisys Track Manager. Copyright 2015-2018 Qualisys AB
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QTMRealTimeSDK;
using System.IO;

namespace QualisysRealTime.Unity
{
    public class RTSkeleton : MonoBehaviour
    {
        public string SkeletonName = "SR";

        private Avatar mSourceAvatar;
        public Avatar DestinationAvatar;

        public bool PoseFingers = true;
        public bool PoseJawJoint = false;

        private HumanPose mHumanPose = new HumanPose();
        private GameObject mStreamedRootObject;
        private Dictionary<uint, GameObject> mQTmSegmentIdToGameObject;
        private Dictionary<string, string> mMecanimToQtmSegmentNames = new Dictionary<string, string>();
        private Dictionary<string, string> mMecanimToQtmFingerSegmentNames = new Dictionary<string, string>();

        private HumanPoseHandler mSourcePoseHandler;
        private HumanPoseHandler mDestiationPoseHandler;

        protected RTClient rtClient;
        private Skeleton mQtmSkeletonCache;
        private Vector3 mFingerBoneTranslation = new Vector3(0.0f, 0.04f, 0.0f);
        private Vector3 mLeftThumbBoneTranslation = new Vector3(-0.04f, 0.0f, 0.04f);
        private Vector3 mRightThumbBoneTranslation = new Vector3(0.04f, 0.0f, -0.04f); 

        void Update()
        {
            if (rtClient == null) rtClient = RTClient.GetInstance();

            Skeleton skeleton = rtClient.GetSkeleton(SkeletonName);

            if (mQtmSkeletonCache != skeleton)
            {
                mQtmSkeletonCache = skeleton;

                if (mQtmSkeletonCache == null)
                    return;

                if (PoseJawJoint)
                {
                    // Add dummy Jaw segment to mQtmSkeletonCache (QTM streamed skeleton)
                    uint parentId = 0;
                    for (int index = 0; index < mQtmSkeletonCache.Segments.Count; index++)
                    {
                        Segment segment = mQtmSkeletonCache.Segments[mQtmSkeletonCache.Segments.Keys.ElementAt(index)];
                        if (segment.Name == "Head") parentId = segment.Id;
                    }
                    if (parentId != 0)
                    {
                        Segment JawSegment = new Segment
                        {
                            Name = "Jaw",
                            Id = 0,
                            ParentId = parentId,
                            Position = Vector3.zero,
                            Rotation = Quaternion.identity,
                            TPosition = Vector3.zero,
                            TRotation = Quaternion.identity
                        };
                        mQtmSkeletonCache.Segments.Add((uint)mQtmSkeletonCache.Segments.Count, JawSegment);
                    }
                    else
                    {
                        Debug.Log("Error: Unable to find index of Head segment in streamed skeleton. Jaw segment not added.");
                    }
                }

                if (PoseFingers)
                {
                    // Build a dictionary for naming finger segments
                    StreamReader stream = File.OpenText("./Assets/Qualisys-Unity-SDK/Qualisys/DemoAssets/Models/FingersMappings/Qualisys.txt");
                    string lineFromFile;
                    while ((lineFromFile = stream.ReadLine()) != null)
                    {
                        int commaPosition = lineFromFile.IndexOf(",");
                        string mecanimBoneName = lineFromFile.Substring(0, commaPosition);
                        string mappedBoneName = lineFromFile.Substring(commaPosition + 1);
                        mMecanimToQtmFingerSegmentNames.Add(mecanimBoneName, mappedBoneName);
                    }
                    stream.Close();

                    // Add dummy Segments for left hand fingers to mQtmSkeletonCache (QTM streamed skeleton)
                    uint parentId = 0;
                    uint boneID = 30000;
                    for (int index = 0; index < mQtmSkeletonCache.Segments.Count; index++)
                    {
                        Segment segment = mQtmSkeletonCache.Segments[mQtmSkeletonCache.Segments.Keys.ElementAt(index)];
                        if (segment.Name == "LeftHand") parentId = segment.Id;
                    }

                    if (parentId != 0)
                    {
                        foreach (var FingerMap in mMecanimToQtmFingerSegmentNames.ToList())
                        {
                            string mecanimBoneName = FingerMap.Key.ToString();
                            string mappedBoneName = FingerMap.Value.ToString();
                            uint fingersParenting = parentId;

                            if (!mecanimBoneName.Contains("Proximal"))
                                fingersParenting = boneID - 1;

                            if (mecanimBoneName.Contains("Left"))
                            {
                                Segment FingerSegment = new Segment
                                {
                                    Name = mappedBoneName,
                                    Id = boneID,
                                    ParentId = fingersParenting,
                                    Position = Vector3.zero,
                                    Rotation = Quaternion.identity,
                                    TPosition = mecanimBoneName.Contains("Thumb") ? mLeftThumbBoneTranslation : mFingerBoneTranslation,
                                    TRotation = Quaternion.identity
                                };
                                mQtmSkeletonCache.Segments.Add((uint)mQtmSkeletonCache.Segments.Count, FingerSegment);
                                boneID++;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Error: Unable to find index of LeftHand segment in streamed skeleton");
                    }

                    // Add dummy Segments for right hand fingers to mQtmSkeletonCache (QTM streamed skeleton)
                    parentId = 0;
                    for (int index = 0; index < mQtmSkeletonCache.Segments.Count; index++)
                    {
                        Segment segment = mQtmSkeletonCache.Segments[mQtmSkeletonCache.Segments.Keys.ElementAt(index)];
                        if (segment.Name == "RightHand") parentId = segment.Id;
                    }

                    if (parentId != 0)
                    {
                        foreach (var FingerMap in mMecanimToQtmFingerSegmentNames.ToList())
                        {
                            string mecanimBoneName = FingerMap.Key.ToString();
                            string mappedBoneName = FingerMap.Value.ToString();
                            uint fingersParenting = parentId;

                            if (!mecanimBoneName.Contains("Proximal"))
                                fingersParenting = boneID - 1;

                            if (mecanimBoneName.Contains("Right"))
                            {
                                Segment FingerSegment = new Segment
                                {
                                    Name = mappedBoneName,
                                    Id = boneID,
                                    ParentId = fingersParenting,
                                    Position = Vector3.zero,
                                    Rotation = Quaternion.identity,
                                    TPosition = mecanimBoneName.Contains("Thumb") ? mRightThumbBoneTranslation : mFingerBoneTranslation,
                                    TRotation = Quaternion.identity
                                };
                                mQtmSkeletonCache.Segments.Add((uint)mQtmSkeletonCache.Segments.Count, FingerSegment);
                                boneID++;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Error: Unable to find index of RightHand segment in streamed skeleton");
                    }
                }

                CreateMecanimToQtmSegmentNames(SkeletonName);

                if (mStreamedRootObject != null)
                    GameObject.Destroy(mStreamedRootObject);

                mStreamedRootObject = new GameObject(this.SkeletonName);

                mQTmSegmentIdToGameObject = new Dictionary<uint, GameObject>(mQtmSkeletonCache.Segments.Count);

                foreach (var segment in mQtmSkeletonCache.Segments.ToList())
                {
                    var gameObject = new GameObject(this.SkeletonName + "_" + segment.Value.Name);
                    gameObject.transform.parent = segment.Value.ParentId == 0 ? mStreamedRootObject.transform : mQTmSegmentIdToGameObject[segment.Value.ParentId].transform;
                    gameObject.transform.localPosition = segment.Value.TPosition;
                    mQTmSegmentIdToGameObject[segment.Value.Id] = gameObject;
                }

                BuildMecanimAvatarFromQtmTPose();

                mStreamedRootObject.transform.SetParent(this.transform, false);
                mStreamedRootObject.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                return;
            } // end of new streamed skeleton setup

            if (mQtmSkeletonCache == null)
                return;

            // Update all the game objects
            foreach (var segment in mQtmSkeletonCache.Segments.ToList())
            {
                GameObject gameObject;
                if (mQTmSegmentIdToGameObject.TryGetValue(segment.Key, out gameObject))
                {
                    gameObject.transform.localPosition = segment.Value.Position;
                    gameObject.transform.localRotation = segment.Value.Rotation;
                }
            }
            if (mSourcePoseHandler != null && mDestiationPoseHandler != null)
            {
                mSourcePoseHandler.GetHumanPose(ref mHumanPose);
                mDestiationPoseHandler.SetHumanPose(ref mHumanPose);
            }
        }

        private void BuildMecanimAvatarFromQtmTPose()
        {
            var humanBones = new List<HumanBone>(mQtmSkeletonCache.Segments.Count);
            for (int index = 0; index < HumanTrait.BoneName.Length; index++)
            {
                var humanBoneName = HumanTrait.BoneName[index];
                if (mMecanimToQtmSegmentNames.ContainsKey(humanBoneName))
                {
                    var bone = new HumanBone()
                    {
                        humanName = humanBoneName,
                        boneName = mMecanimToQtmSegmentNames[humanBoneName],
                    };
                    bone.limit.useDefaultValues = true;
                    humanBones.Add(bone);
                }
            }

            // Set up the T-pose and game object name mappings.
            var skeletonBones = new List<SkeletonBone>(mQtmSkeletonCache.Segments.Count + 1);
            skeletonBones.Add(new SkeletonBone()
            {
                name = this.SkeletonName,
                position = Vector3.zero,
                rotation = Quaternion.identity,
                scale = Vector3.one,
            });

            // Create remaining T-Pose bone definitions from Qtm segments
            foreach (var segment in mQtmSkeletonCache.Segments.ToList())
            {
                skeletonBones.Add(new SkeletonBone()
                {
                    name = this.SkeletonName + "_" + segment.Value.Name,
                    position = segment.Value.TPosition,
                    rotation = Quaternion.identity,
                    scale = Vector3.one,
                });

                if (segment.Value.Name == "Head" && PoseJawJoint)
                {
                    // Add the Jaw to the source avatar's skeletonBones
                    skeletonBones.Add(new SkeletonBone()
                    {
                        name = "Jaw",
                        position = segment.Value.TPosition,
                        rotation = Quaternion.identity,
                        scale = Vector3.one,
                    });
                }
                else if (segment.Value.Name == "LeftHand" && PoseFingers)
                {
                    // Add the left hand's finger joints to the source avatar's skeletonBones
                    foreach (var FingerMap in mMecanimToQtmFingerSegmentNames.ToList())
                    {
                        string boneName = FingerMap.Key.ToString();
                        if (boneName.Contains("Left"))
                        {
                            skeletonBones.Add(new SkeletonBone()
                            {
                                name = boneName,
                                position = segment.Value.TPosition,
                                rotation = Quaternion.identity,
                                scale = Vector3.one,
                            });
                        }
                    }
                }
                else if (segment.Value.Name == "RightHand" && PoseFingers)
                {
                    // Add the right hand's finger joints to the source avatar's skeletonBones
                    foreach (var FingerMap in mMecanimToQtmFingerSegmentNames.ToList())
                    {
                        string boneName = FingerMap.Key.ToString();
                        if (boneName.Contains("Right"))
                        {
                            skeletonBones.Add(new SkeletonBone()
                            {
                                name = boneName,
                                position = segment.Value.TPosition,
                                rotation = Quaternion.identity,
                                scale = Vector3.one,
                            });
                        }
                    }
                }
            }

            mSourceAvatar = AvatarBuilder.BuildHumanAvatar(mStreamedRootObject,
                new HumanDescription()
                {
                    human = humanBones.ToArray(),
                    skeleton = skeletonBones.ToArray(),
                    //feetSpacing = FeetSpacing   // Doesn't seem to work, no idea why
                }
            );
            if (mSourceAvatar.isValid == false || mSourceAvatar.isHuman == false)
            {
                this.enabled = false;
                return;
            }

            mSourcePoseHandler = new HumanPoseHandler(mSourceAvatar, mStreamedRootObject.transform);
            mDestiationPoseHandler = new HumanPoseHandler(DestinationAvatar, this.transform);
        }

        private void CreateMecanimToQtmSegmentNames(string skeletonName)
        {
            mMecanimToQtmSegmentNames.Clear();
            mMecanimToQtmSegmentNames.Add("RightShoulder", skeletonName + "_RightShoulder");
            mMecanimToQtmSegmentNames.Add("RightUpperArm", skeletonName + "_RightArm");
            mMecanimToQtmSegmentNames.Add("RightLowerArm", skeletonName + "_RightForeArm");
            mMecanimToQtmSegmentNames.Add("RightHand", skeletonName + "_RightHand");
            mMecanimToQtmSegmentNames.Add("LeftShoulder", skeletonName + "_LeftShoulder");
            mMecanimToQtmSegmentNames.Add("LeftUpperArm", skeletonName + "_LeftArm");
            mMecanimToQtmSegmentNames.Add("LeftLowerArm", skeletonName + "_LeftForeArm");
            mMecanimToQtmSegmentNames.Add("LeftHand", skeletonName + "_LeftHand");

            mMecanimToQtmSegmentNames.Add("RightUpperLeg", skeletonName + "_RightUpLeg");
            mMecanimToQtmSegmentNames.Add("RightLowerLeg", skeletonName + "_RightLeg");
            mMecanimToQtmSegmentNames.Add("RightFoot", skeletonName + "_RightFoot");
            mMecanimToQtmSegmentNames.Add("RightToeBase", skeletonName + "_RightToeBase");
            mMecanimToQtmSegmentNames.Add("LeftUpperLeg", skeletonName + "_LeftUpLeg");
            mMecanimToQtmSegmentNames.Add("LeftLowerLeg", skeletonName + "_LeftLeg");
            mMecanimToQtmSegmentNames.Add("LeftFoot", skeletonName + "_LeftFoot");
            mMecanimToQtmSegmentNames.Add("LeftToeBase", skeletonName + "_LeftToeBase");

            mMecanimToQtmSegmentNames.Add("Hips", skeletonName + "_Hips");
            mMecanimToQtmSegmentNames.Add("Spine", skeletonName + "_Spine");
            mMecanimToQtmSegmentNames.Add("Chest", skeletonName + "_Spine1");
            mMecanimToQtmSegmentNames.Add("UpperChest", skeletonName + "_Spine2");
            mMecanimToQtmSegmentNames.Add("Neck", skeletonName + "_Neck");
            mMecanimToQtmSegmentNames.Add("Head", skeletonName + "_Head");

            if (PoseJawJoint)
            {
                mMecanimToQtmSegmentNames.Add("Jaw", skeletonName + "_Jaw");
            }

            if (PoseFingers)
            {
                mMecanimToQtmSegmentNames.Add("Left Thumb Proximal", skeletonName + "_LeftHandThumb1");
                mMecanimToQtmSegmentNames.Add("Left Thumb Intermediate", skeletonName + "_LeftHandThumb2");
                mMecanimToQtmSegmentNames.Add("Left Thumb Distal", skeletonName + "_LeftHandThumb3");
                mMecanimToQtmSegmentNames.Add("Left Index Proximal", skeletonName + "_LeftHandIndex1");
                mMecanimToQtmSegmentNames.Add("Left Index Intermediate", skeletonName + "_LeftHandIndex2");
                mMecanimToQtmSegmentNames.Add("Left Index Distal", skeletonName + "_LeftHandIndex3");
                mMecanimToQtmSegmentNames.Add("Left Middle Proximal", skeletonName + "_LeftHandMiddle1");
                mMecanimToQtmSegmentNames.Add("Left Middle Intermediate", skeletonName + "_LeftHandMiddle2");
                mMecanimToQtmSegmentNames.Add("Left Middle Distal", skeletonName + "_LeftHandMiddle3");
                mMecanimToQtmSegmentNames.Add("Left Ring Proximal", skeletonName + "_LeftHandRing1");
                mMecanimToQtmSegmentNames.Add("Left Ring Intermediate", skeletonName + "_LeftHandRing2");
                mMecanimToQtmSegmentNames.Add("Left Ring Distal", skeletonName + "_LeftHandRing3");
                mMecanimToQtmSegmentNames.Add("Left Little Proximal", skeletonName + "_LeftHandPinky1");
                mMecanimToQtmSegmentNames.Add("Left Little Intermediate", skeletonName + "_LeftHandPinky2");
                mMecanimToQtmSegmentNames.Add("Left Little Distal", skeletonName + "_LeftHandPinky3");

                mMecanimToQtmSegmentNames.Add("Right Thumb Proximal", skeletonName + "_RightHandThumb1");
                mMecanimToQtmSegmentNames.Add("Right Thumb Intermediate", skeletonName + "_RightHandThumb2");
                mMecanimToQtmSegmentNames.Add("Right Thumb Distal", skeletonName + "_RightHandThumb3");
                mMecanimToQtmSegmentNames.Add("Right Index Proximal", skeletonName + "_RightHandIndex1");
                mMecanimToQtmSegmentNames.Add("Right Index Intermediate", skeletonName + "_RightHandIndex2");
                mMecanimToQtmSegmentNames.Add("Right Index Distal", skeletonName + "_RightHandIndex3");
                mMecanimToQtmSegmentNames.Add("Right Middle Proximal", skeletonName + "_RightHandMiddle1");
                mMecanimToQtmSegmentNames.Add("Right Middle Intermediate", skeletonName + "_RightHandMiddle2");
                mMecanimToQtmSegmentNames.Add("Right Middle Distal", skeletonName + "_RightHandMiddle3");
                mMecanimToQtmSegmentNames.Add("Right Ring Proximal", skeletonName + "_RightHandRing1");
                mMecanimToQtmSegmentNames.Add("Right Ring Intermediate", skeletonName + "_RightHandRing2");
                mMecanimToQtmSegmentNames.Add("Right Ring Distal", skeletonName + "_RightHandRing3");
                mMecanimToQtmSegmentNames.Add("Right Little Proximal", skeletonName + "_RightHandPinky1");
                mMecanimToQtmSegmentNames.Add("Right Little Intermediate", skeletonName + "_RightHandPinky2");
                mMecanimToQtmSegmentNames.Add("Right Little Distal", skeletonName + "_RightHandPinky3");
            }
        }
    }
}