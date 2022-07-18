using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Jirko.Unity.VRoidAvatarUtils;
using VRC.SDK3.Avatars.Components;
using VRC.Core;

namespace Tests
{
    public class PipelineManagerTest
    {
        GameObject sourceObject;
        GameObject targetObject;

        [SetUp]
        public void Setup()
        {
            GameObject prefab = (GameObject)Resources.Load("SourceObject");
            sourceObject = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            prefab = (GameObject)Resources.Load("TargetObject");
            targetObject = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
        [TearDown]
        public void TearDown()
        {
            GameObject.DestroyImmediate(sourceObject);
            GameObject.DestroyImmediate(targetObject);
        }

        [UnityTest]
        public IEnumerator PipelineManagerがある場合blueprintIdをコピーできる()
        {
            PipelineManager sourcePipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();
            sourcePipelineManager.blueprintId = "dummy";

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = true;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            PipelineManager targetPipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();

            Assert.That(sourceAvatar.errors.Count == 0);
            Assert.That(targetPipelineManager.blueprintId, Is.EqualTo("dummy"));
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("BlueprintIDをコピー"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator SourceのPipelineManagerがない場合blueprintIdをコピーできない()
        {
            PipelineManager sourcePipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(sourcePipelineManager);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = true;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            PipelineManager targetPipelineManager = (PipelineManager)targetObject.GetComponent<PipelineManager>();

            Assert.That(sourceAvatar.errors.Count == 1);
            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(targetPipelineManager.blueprintId, Is.EqualTo("target_dummy"));
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー元アバターにコンポーネントを追加してください"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator TargetのPipelineManagerがない場合blueprintIdをコピーできない()
        {
            PipelineManager targetPipelineManager = (PipelineManager)targetObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(targetPipelineManager);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = true;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            targetPipelineManager = (PipelineManager)targetObject.GetComponent<PipelineManager>();

            Assert.That(sourceAvatar.errors.Count == 1);
            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(targetPipelineManager == null);
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー先アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー先アバターにコンポーネントを追加してください"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator SourceとTargetともにPipelineManagerがない場合blueprintIdをコピーできない()
        {
            PipelineManager sourcePipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(sourcePipelineManager);
            PipelineManager targetPipelineManager = (PipelineManager)targetObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(targetPipelineManager);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = true;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            targetPipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();

            Assert.That(sourceAvatar.errors.Count == 2);
            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(targetPipelineManager == null);
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー元アバターにコンポーネントを追加してください"));
            Assert.That(sourceAvatar.errors[1], Is.EqualTo("コピー先アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー先アバターにコンポーネントを追加してください"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator PipelineManagerがなくてもphysBonesはコピーできる()
        {
            PipelineManager sourcePipelineManager = (PipelineManager)sourceObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(sourcePipelineManager);
            PipelineManager targetPipelineManager = (PipelineManager)targetObject.GetComponent<PipelineManager>();
            GameObject.DestroyImmediate(targetPipelineManager);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = true;
            sourceAvatar.physBones = true;
            sourceAvatar.physBones_hair = true;
            sourceAvatar.physBones_skirt = true;
            sourceAvatar.physBones_bust = true;
            sourceAvatar.physBones_sleeve = true;
            sourceAvatar.physBones_other = true;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = false;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            Assert.That(sourceAvatar.errors.Count, Is.EqualTo(2));
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー元アバターにコンポーネントを追加してください"));
            Assert.That(sourceAvatar.errors[1], Is.EqualTo("コピー先アバターにPipelineManagerコンポーネントがないためコピーできませんでした。\n Blueprint IDのチェックを外すか、コピー先アバターにコンポーネントを追加してください"));

            Assert.That(sourceAvatar.messages.Count, Is.EqualTo(1));
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Phys Boneをコピー（24件）"));


            yield return null;
        }
    }
}
