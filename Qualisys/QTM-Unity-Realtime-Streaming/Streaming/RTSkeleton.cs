// Unity SDK for Qualisys Track Manager. Copyright 2015-2018 Qualisys AB
//
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEngine.Experimental.Animations;
using QTMRealTimeSDK;
using QualisysRealTime.Unity;

namespace QualisysRealTime.Unity
{
    public struct AnimationJob : IAnimationJob
    {
        public TransformStreamHandle transformHandle;
        public Vector3 localTranslation;
        public Quaternion localRotation;

        // Jobs run on a separate thread (usually on a separate core)
        // Because of this, we cannot use references
        // Since arrays are accessed by reference we can't use them inside this job
        // I suspect there is a more elegant way of handling all these muscles, but this is just a prototype for now
        public MuscleHandle SpineFrontBack;
        public MuscleHandle SpineLeftRight;
        public MuscleHandle SpineTwistLeftRight;
        public MuscleHandle ChestFrontBack;
        public MuscleHandle ChestLeftRight;
        public MuscleHandle ChestTwistLeftRight;
        public MuscleHandle UpperChestFrontBack;
        public MuscleHandle UpperChestLeftRight;
        public MuscleHandle UpperChestTwistLeftRight;
        public MuscleHandle NeckNodDownUp;
        public MuscleHandle NeckTiltLeftRight;
        public MuscleHandle NeckTurnLeftRight;
        public MuscleHandle HeadNodDownUp;
        public MuscleHandle HeadTiltLeftRight;
        public MuscleHandle HeadTurnLeftRight;
        public MuscleHandle LeftUpperLegFrontBack;
        public MuscleHandle LeftUpperLegInOut;
        public MuscleHandle LeftUpperLegTwistInOut;
        public MuscleHandle LeftLowerLegStretch;
        public MuscleHandle LeftLowerLegTwistInOut;
        public MuscleHandle LeftFootUpDown;
        public MuscleHandle LeftFootTwistInOut;
        public MuscleHandle LeftToesUpDown;
        public MuscleHandle RightUpperLegFrontBack;
        public MuscleHandle RightUpperLegInOut;
        public MuscleHandle RightUpperLegTwistInOut;
        public MuscleHandle RightLowerLegStretch;
        public MuscleHandle RightLowerLegTwistInOut;
        public MuscleHandle RightFootUpDown;
        public MuscleHandle RightFootTwistInOut;
        public MuscleHandle RightToesUpDown;
        public MuscleHandle LeftShoulderDownUp;
        public MuscleHandle LeftShoulderFrontBack;
        public MuscleHandle LeftArmDownUp;
        public MuscleHandle LeftArmFrontBack;
        public MuscleHandle LeftArmTwistInOut;
        public MuscleHandle LeftForearmStretch;
        public MuscleHandle LeftForearmTwistInOut;
        public MuscleHandle LeftHandDownUp;
        public MuscleHandle LeftHandInOut;
        public MuscleHandle RightShoulderDownUp;
        public MuscleHandle RightShoulderFrontBack;
        public MuscleHandle RightArmDownUp;
        public MuscleHandle RightArmFrontBack;
        public MuscleHandle RightArmTwistInOut;
        public MuscleHandle RightForearmStretch;
        public MuscleHandle RightForearmTwistInOut;
        public MuscleHandle RightHandDownUp;
        public MuscleHandle RightHandInOut;

        public float SpineFrontBackValue;
        public float SpineLeftRightValue;
        public float SpineTwistLeftRightValue;
        public float ChestFrontBackValue;
        public float ChestLeftRightValue;
        public float ChestTwistLeftRightValue;
        public float UpperChestFrontBackValue;
        public float UpperChestLeftRightValue;
        public float UpperChestTwistLeftRightValue;
        public float NeckNodDownUpValue;
        public float NeckTiltLeftRightValue;
        public float NeckTurnLeftRightValue;
        public float HeadNodDownUpValue;
        public float HeadTiltLeftRightValue;
        public float HeadTurnLeftRightValue;
        public float LeftUpperLegFrontBackValue;
        public float LeftUpperLegInOutValue;
        public float LeftUpperLegTwistInOutValue;
        public float LeftLowerLegStretchValue;
        public float LeftLowerLegTwistInOutValue;
        public float LeftFootUpDownValue;
        public float LeftFootTwistInOutValue;
        public float LeftToesUpDownValue;
        public float RightUpperLegFrontBackValue;
        public float RightUpperLegInOutValue;
        public float RightUpperLegTwistInOutValue;
        public float RightLowerLegStretchValue;
        public float RightLowerLegTwistInOutValue;
        public float RightFootUpDownValue;
        public float RightFootTwistInOutValue;
        public float RightToesUpDownValue;
        public float LeftShoulderDownUpValue;
        public float LeftShoulderFrontBackValue;
        public float LeftArmDownUpValue;
        public float LeftArmFrontBackValue;
        public float LeftArmTwistInOutValue;
        public float LeftForearmStretchValue;
        public float LeftForearmTwistInOutValue;
        public float LeftHandDownUpValue;
        public float LeftHandInOutValue;
        public float RightShoulderDownUpValue;
        public float RightShoulderFrontBackValue;
        public float RightArmDownUpValue;
        public float RightArmFrontBackValue;
        public float RightArmTwistInOutValue;
        public float RightForearmStretchValue;
        public float RightForearmTwistInOutValue;
        public float RightHandDownUpValue;
        public float RightHandInOutValue;

        public void ProcessRootMotion(AnimationStream stream)
        {
            // Set the new local position
            transformHandle.SetLocalPosition(stream, localTranslation);

            // Set the new local rotation
            transformHandle.SetLocalRotation(stream, localRotation);
        }

        public void ProcessAnimation(AnimationStream stream)
        {
            AnimationHumanStream humanStream = stream.AsHuman();

            // Set muscle values
            humanStream.SetMuscle(SpineFrontBack, SpineFrontBackValue);
            humanStream.SetMuscle(SpineLeftRight, SpineLeftRightValue);
            humanStream.SetMuscle(SpineTwistLeftRight, SpineTwistLeftRightValue);
            humanStream.SetMuscle(ChestFrontBack, ChestFrontBackValue);
            humanStream.SetMuscle(ChestLeftRight, ChestLeftRightValue);
            humanStream.SetMuscle(ChestTwistLeftRight, ChestTwistLeftRightValue);
            humanStream.SetMuscle(UpperChestFrontBack, UpperChestFrontBackValue);
            humanStream.SetMuscle(UpperChestLeftRight, UpperChestLeftRightValue);
            humanStream.SetMuscle(UpperChestTwistLeftRight, UpperChestTwistLeftRightValue);
            humanStream.SetMuscle(NeckNodDownUp, NeckNodDownUpValue);
            humanStream.SetMuscle(NeckTiltLeftRight, NeckTiltLeftRightValue);
            humanStream.SetMuscle(NeckTurnLeftRight, NeckTurnLeftRightValue);
            humanStream.SetMuscle(HeadNodDownUp, HeadNodDownUpValue);
            humanStream.SetMuscle(HeadTiltLeftRight, HeadTiltLeftRightValue);
            humanStream.SetMuscle(HeadTurnLeftRight, HeadTurnLeftRightValue);
            humanStream.SetMuscle(LeftUpperLegFrontBack, LeftUpperLegFrontBackValue);
            humanStream.SetMuscle(LeftUpperLegInOut, LeftUpperLegInOutValue);
            humanStream.SetMuscle(LeftUpperLegTwistInOut, LeftUpperLegTwistInOutValue);
            humanStream.SetMuscle(LeftLowerLegStretch, LeftLowerLegStretchValue);
            humanStream.SetMuscle(LeftLowerLegTwistInOut, LeftLowerLegTwistInOutValue);
            humanStream.SetMuscle(LeftFootUpDown, LeftFootUpDownValue);
            humanStream.SetMuscle(LeftFootTwistInOut, LeftFootTwistInOutValue);
            humanStream.SetMuscle(LeftToesUpDown, LeftToesUpDownValue);
            humanStream.SetMuscle(RightUpperLegFrontBack, RightUpperLegFrontBackValue); //Debug.Log("Set upper leg FB value to " + RightUpperLegInOutValue);
            humanStream.SetMuscle(RightUpperLegInOut, RightUpperLegInOutValue);
            humanStream.SetMuscle(RightUpperLegTwistInOut, RightUpperLegTwistInOutValue);
            humanStream.SetMuscle(RightLowerLegStretch, RightLowerLegStretchValue);
            humanStream.SetMuscle(RightLowerLegTwistInOut, RightLowerLegTwistInOutValue);
            humanStream.SetMuscle(RightFootUpDown, RightFootUpDownValue);
            humanStream.SetMuscle(RightFootTwistInOut, RightFootTwistInOutValue);
            humanStream.SetMuscle(RightToesUpDown, RightToesUpDownValue);
            humanStream.SetMuscle(LeftShoulderDownUp, LeftShoulderDownUpValue);
            humanStream.SetMuscle(LeftShoulderFrontBack, LeftShoulderFrontBackValue);
            humanStream.SetMuscle(LeftArmDownUp, LeftArmDownUpValue);
            humanStream.SetMuscle(LeftArmFrontBack, LeftArmFrontBackValue);
            humanStream.SetMuscle(LeftArmTwistInOut, LeftArmTwistInOutValue);
            humanStream.SetMuscle(LeftForearmStretch, LeftForearmStretchValue);
            humanStream.SetMuscle(LeftForearmTwistInOut, LeftForearmTwistInOutValue);
            humanStream.SetMuscle(LeftHandDownUp, LeftHandDownUpValue);
            humanStream.SetMuscle(LeftHandInOut, LeftHandInOutValue);
            humanStream.SetMuscle(RightShoulderDownUp, RightShoulderDownUpValue);
            humanStream.SetMuscle(RightShoulderFrontBack, RightShoulderFrontBackValue);
            humanStream.SetMuscle(RightArmDownUp, RightArmDownUpValue);
            humanStream.SetMuscle(RightArmFrontBack, RightArmFrontBackValue);
            humanStream.SetMuscle(RightArmTwistInOut, RightArmTwistInOutValue);
            humanStream.SetMuscle(RightForearmStretch, RightForearmStretchValue);
            humanStream.SetMuscle(RightForearmTwistInOut, RightForearmTwistInOutValue);
            humanStream.SetMuscle(RightHandDownUp, RightHandDownUpValue);
            humanStream.SetMuscle(RightHandInOut, RightHandInOutValue);
        }
    }

    [RequireComponent(typeof(Animator))]
    public class RTSkeleton : MonoBehaviour
    {
        // Use Playables API and animation struct implementing IAnimationJob (part of Unity's excperimental animation features)
        private AnimationJob mAnimationJob;
        private PlayableGraph mPlayableGraph;
        private AnimationScriptPlayable mAnimationScriptPlayable;
        private AnimationMixerPlayable mMixerPlayable;

        public string SkeletonName = "SR";
        protected RTClient rtClient;
        private Skeleton mQtmSkeletonCache;

        private Avatar mSourceAvatar;
        //public Avatar DestinationAvatar;

        private HumanPose mHumanPose = new HumanPose();
        private GameObject mStreamedRootObject;
        private Dictionary<uint, GameObject> mQTmSegmentIdToGameObject;
        private Dictionary<string, string> mMecanimToQtmSegmentNames = new Dictionary<string, string>();
        private HumanPoseHandler mSourcePoseHandler;

        void OnEnable()
        {
            // Create graph
            mPlayableGraph = PlayableGraph.Create("AnimationScriptExample");

            // Connect output to Animator
            Animator animator = GetComponent<Animator>();
            var playableOutput = AnimationPlayableOutput.Create(mPlayableGraph, "Animation", animator);

            // Create animation job for streamed data
            mAnimationJob = new AnimationJob();

            // Bind the transform stream
            mAnimationJob.transformHandle = animator.BindStreamTransform(gameObject.transform);

            // Get handles on all the muscles driven by the QTM skeleton stream
            mAnimationJob.SpineFrontBack = new MuscleHandle(BodyDof.SpineFrontBack);
            mAnimationJob.SpineLeftRight = new MuscleHandle(BodyDof.SpineLeftRight);
            mAnimationJob.SpineTwistLeftRight = new MuscleHandle(BodyDof.SpineRollLeftRight);
            mAnimationJob.ChestFrontBack = new MuscleHandle(BodyDof.ChestFrontBack);
            mAnimationJob.ChestLeftRight = new MuscleHandle(BodyDof.ChestLeftRight);
            mAnimationJob.ChestTwistLeftRight = new MuscleHandle(BodyDof.ChestRollLeftRight);
            mAnimationJob.UpperChestFrontBack = new MuscleHandle(BodyDof.UpperChestFrontBack);
            mAnimationJob.UpperChestLeftRight = new MuscleHandle(BodyDof.UpperChestLeftRight);
            mAnimationJob.UpperChestTwistLeftRight = new MuscleHandle(BodyDof.UpperChestRollLeftRight);
            mAnimationJob.NeckNodDownUp = new MuscleHandle(HeadDof.NeckFrontBack);
            mAnimationJob.NeckTiltLeftRight = new MuscleHandle(HeadDof.NeckLeftRight);
            mAnimationJob.NeckTurnLeftRight = new MuscleHandle(HeadDof.NeckRollLeftRight);
            mAnimationJob.HeadNodDownUp = new MuscleHandle(HeadDof.HeadFrontBack);
            mAnimationJob.HeadTiltLeftRight = new MuscleHandle(HeadDof.HeadLeftRight);
            mAnimationJob.HeadTurnLeftRight = new MuscleHandle(HeadDof.HeadRollLeftRight);
            mAnimationJob.LeftUpperLegFrontBack = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.UpperLegFrontBack);
            mAnimationJob.LeftUpperLegInOut = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.UpperLegInOut);
            mAnimationJob.LeftUpperLegTwistInOut = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.UpperLegRollInOut);
            mAnimationJob.LeftLowerLegStretch = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.LegCloseOpen);
            mAnimationJob.LeftLowerLegTwistInOut = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.LegRollInOut);
            mAnimationJob.LeftFootUpDown = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.FootCloseOpen);
            mAnimationJob.LeftFootTwistInOut = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.FootInOut);
            mAnimationJob.LeftToesUpDown = new MuscleHandle(HumanPartDof.LeftLeg, LegDof.ToesUpDown);
            mAnimationJob.RightUpperLegFrontBack = new MuscleHandle(HumanPartDof.RightLeg, LegDof.UpperLegFrontBack);
            mAnimationJob.RightUpperLegInOut = new MuscleHandle(HumanPartDof.RightLeg, LegDof.UpperLegInOut);
            mAnimationJob.RightUpperLegTwistInOut = new MuscleHandle(HumanPartDof.RightLeg, LegDof.UpperLegRollInOut);
            mAnimationJob.RightLowerLegStretch = new MuscleHandle(HumanPartDof.RightLeg, LegDof.LegCloseOpen);
            mAnimationJob.RightLowerLegTwistInOut = new MuscleHandle(HumanPartDof.RightLeg, LegDof.LegRollInOut);
            mAnimationJob.RightFootUpDown = new MuscleHandle(HumanPartDof.RightLeg, LegDof.FootCloseOpen);
            mAnimationJob.RightFootTwistInOut = new MuscleHandle(HumanPartDof.RightLeg, LegDof.FootInOut);
            mAnimationJob.RightToesUpDown = new MuscleHandle(HumanPartDof.RightLeg, LegDof.ToesUpDown);
            mAnimationJob.LeftShoulderDownUp = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ShoulderDownUp);
            mAnimationJob.LeftShoulderFrontBack = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ShoulderFrontBack);
            mAnimationJob.LeftArmDownUp = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ArmDownUp);
            mAnimationJob.LeftArmFrontBack = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ArmFrontBack);
            mAnimationJob.LeftArmTwistInOut = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ArmRollInOut);
            mAnimationJob.LeftForearmStretch = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ForeArmCloseOpen);
            mAnimationJob.LeftForearmTwistInOut = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.ForeArmRollInOut);
            mAnimationJob.LeftHandDownUp = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.HandDownUp);
            mAnimationJob.LeftHandInOut = new MuscleHandle(HumanPartDof.LeftArm, ArmDof.HandInOut);
            mAnimationJob.RightShoulderDownUp = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ShoulderDownUp);
            mAnimationJob.RightShoulderFrontBack = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ShoulderFrontBack);
            mAnimationJob.RightArmDownUp = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ArmDownUp);
            mAnimationJob.RightArmFrontBack = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ArmFrontBack);
            mAnimationJob.RightArmTwistInOut = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ArmRollInOut);
            mAnimationJob.RightForearmStretch = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ForeArmCloseOpen);
            mAnimationJob.RightForearmTwistInOut = new MuscleHandle(HumanPartDof.RightArm, ArmDof.ForeArmRollInOut);
            mAnimationJob.RightHandDownUp = new MuscleHandle(HumanPartDof.RightArm, ArmDof.HandDownUp);
            mAnimationJob.RightHandInOut = new MuscleHandle(HumanPartDof.RightArm, ArmDof.HandInOut);/**/

            // Create our (animation) script playable
            mAnimationScriptPlayable = AnimationScriptPlayable.Create(mPlayableGraph, mAnimationJob);

            // Create mixer ready for other inputs
            mMixerPlayable = AnimationMixerPlayable.Create(mPlayableGraph, 1);

            // Connect mixer to final output
            playableOutput.SetSourcePlayable(mMixerPlayable);

            // Connect the skeleton stream to the mixer
            mPlayableGraph.Connect(mAnimationScriptPlayable, 0, mMixerPlayable, 0);

            mPlayableGraph.Play();
        }

        void Update()
        {
            if (rtClient == null) rtClient = RTClient.GetInstance();
            var skeleton = rtClient.GetSkeleton(SkeletonName);

            if (mQtmSkeletonCache != skeleton)
            {
                mQtmSkeletonCache = skeleton;

                if (mQtmSkeletonCache == null)
                    return;

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

                BuildMecanimHumanPoseFromQtmTPose();

                mStreamedRootObject.transform.SetParent(this.transform, false);
                mStreamedRootObject.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
                return;
            }

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
            if (mSourcePoseHandler != null)
            {
                // Get a pose handler for the source avatar
                mSourcePoseHandler.GetHumanPose(ref mHumanPose);

                // Get a reference to the animation job
                mAnimationJob = mAnimationScriptPlayable.GetJobData<AnimationJob>();

                // Pass our source avatar's body position and rotation to the animation job
                mAnimationJob.localTranslation = mHumanPose.bodyPosition;
                mAnimationJob.localRotation = mHumanPose.bodyRotation;

                // Pass our source avatar's muscle values to the animation job
                mAnimationJob.SpineFrontBackValue = mHumanPose.muscles[0];
                mAnimationJob.SpineLeftRightValue = mHumanPose.muscles[1];
                mAnimationJob.SpineTwistLeftRightValue = mHumanPose.muscles[2];
                mAnimationJob.ChestFrontBackValue = mHumanPose.muscles[3];
                mAnimationJob.ChestLeftRightValue = mHumanPose.muscles[4];
                mAnimationJob.ChestTwistLeftRightValue = mHumanPose.muscles[5];
                mAnimationJob.UpperChestFrontBackValue = mHumanPose.muscles[6];
                mAnimationJob.UpperChestLeftRightValue = mHumanPose.muscles[7];
                mAnimationJob.UpperChestTwistLeftRightValue = mHumanPose.muscles[8];
                mAnimationJob.NeckNodDownUpValue = mHumanPose.muscles[9];
                mAnimationJob.NeckTiltLeftRightValue = mHumanPose.muscles[10];
                mAnimationJob.NeckTurnLeftRightValue = mHumanPose.muscles[11];
                mAnimationJob.HeadNodDownUpValue = mHumanPose.muscles[12];
                mAnimationJob.HeadTiltLeftRightValue = mHumanPose.muscles[13];
                mAnimationJob.HeadTurnLeftRightValue = mHumanPose.muscles[14];
                mAnimationJob.LeftUpperLegFrontBackValue = mHumanPose.muscles[21];
                mAnimationJob.LeftUpperLegInOutValue = mHumanPose.muscles[22];
                mAnimationJob.LeftUpperLegTwistInOutValue = mHumanPose.muscles[23];
                mAnimationJob.LeftLowerLegStretchValue = mHumanPose.muscles[24];
                mAnimationJob.LeftLowerLegTwistInOutValue = mHumanPose.muscles[25];
                mAnimationJob.LeftFootUpDownValue = mHumanPose.muscles[26];
                mAnimationJob.LeftFootTwistInOutValue = mHumanPose.muscles[27];
                mAnimationJob.LeftToesUpDownValue = mHumanPose.muscles[28];
                mAnimationJob.RightUpperLegFrontBackValue = mHumanPose.muscles[29];
                mAnimationJob.RightUpperLegInOutValue = mHumanPose.muscles[30];
                mAnimationJob.RightUpperLegTwistInOutValue = mHumanPose.muscles[31];
                mAnimationJob.RightLowerLegStretchValue = mHumanPose.muscles[32];
                mAnimationJob.RightLowerLegTwistInOutValue = mHumanPose.muscles[33];
                mAnimationJob.RightFootUpDownValue = mHumanPose.muscles[34];
                mAnimationJob.RightFootTwistInOutValue = mHumanPose.muscles[35];
                mAnimationJob.RightToesUpDownValue = mHumanPose.muscles[36];
                mAnimationJob.LeftShoulderDownUpValue = mHumanPose.muscles[37];
                mAnimationJob.LeftShoulderFrontBackValue = mHumanPose.muscles[38];
                mAnimationJob.LeftArmDownUpValue = mHumanPose.muscles[39];
                mAnimationJob.LeftArmFrontBackValue = mHumanPose.muscles[40];
                mAnimationJob.LeftArmTwistInOutValue = mHumanPose.muscles[41];
                mAnimationJob.LeftForearmStretchValue = mHumanPose.muscles[42];
                mAnimationJob.LeftForearmTwistInOutValue = mHumanPose.muscles[43];
                mAnimationJob.LeftHandDownUpValue = mHumanPose.muscles[44];
                mAnimationJob.LeftHandInOutValue = mHumanPose.muscles[45];
                mAnimationJob.RightShoulderDownUpValue = mHumanPose.muscles[46];
                mAnimationJob.RightShoulderFrontBackValue = mHumanPose.muscles[47];
                mAnimationJob.RightArmDownUpValue = mHumanPose.muscles[48];
                mAnimationJob.RightArmFrontBackValue = mHumanPose.muscles[49];
                mAnimationJob.RightArmTwistInOutValue = mHumanPose.muscles[50];
                mAnimationJob.RightForearmStretchValue = mHumanPose.muscles[51];
                mAnimationJob.RightForearmTwistInOutValue = mHumanPose.muscles[52];
                mAnimationJob.RightHandDownUpValue = mHumanPose.muscles[53];
                mAnimationJob.RightHandInOutValue = mHumanPose.muscles[54];

                // Pass our newly populated job to the script playable
                mAnimationScriptPlayable.SetJobData(mAnimationJob);

                // Set up our mixer (input number zero set to weight 1)
                mMixerPlayable.SetInputWeight(0, 1);

                //mPlayableGraph.Evaluate();
            }
        }

        void OnDisable()
        {
            // Destroys all Playables and Outputs created by the graph.
            mPlayableGraph.Destroy();
        }

        private void BuildMecanimHumanPoseFromQtmTPose()
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
            }

            mSourceAvatar = AvatarBuilder.BuildHumanAvatar(mStreamedRootObject,
                new HumanDescription()
                {
                    human = humanBones.ToArray(),
                    skeleton = skeletonBones.ToArray(),
                }
            );
            if (mSourceAvatar.isValid == false || mSourceAvatar.isHuman == false)
            {
                Debug.Log("Problem with source avatar - aborting Build Mecanim Human Pose From Qtm TPose operation");
                this.enabled = false;
                return;
            }

            mSourcePoseHandler = new HumanPoseHandler(mSourceAvatar, mStreamedRootObject.transform);
            if (mSourcePoseHandler != null)
            {
                mSourcePoseHandler.GetHumanPose(ref mHumanPose);
            }
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
        }
    }
}