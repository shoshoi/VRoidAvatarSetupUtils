using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Jirko.Unity.VRoidAvatarUtils;
using VRC.SDK3.Avatars.Components;

namespace Tests
{
    public class VRCAvatarDescriptorTest
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
        public IEnumerator VRCDescがある場合ViewPositionをコピーできる()
        {
            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode             = 0;
            sourceAvatar.viewPosition           = true;
            sourceAvatar.eyeMovements           = true;
            sourceAvatar.baseAnimationLayers    = true;
            sourceAvatar.specialAnimationLayers = true;
            sourceAvatar.expressionsMenu        = true;
            sourceAvatar.expressionParameters   = true;
            sourceAvatar.rotationStates         = true;
            sourceAvatar.blueprintId            = false;
            sourceAvatar.physBones              = false;
            sourceAvatar.physBones_hair         = false;
            sourceAvatar.physBones_skirt        = false;
            sourceAvatar.physBones_bust         = false;
            sourceAvatar.physBones_sleeve       = false;
            sourceAvatar.physBones_other        = false;
            sourceAvatar.physBoneColiders       = false;
            sourceAvatar.objects                = false;
            sourceAvatar.aimConstraint          = false;
            sourceAvatar.lookAtConstraint       = false;
            sourceAvatar.parentConstraint       = false;
            sourceAvatar.positionConstraint     = false;
            sourceAvatar.rotationConstraint     = false;
            sourceAvatar.scaleConstraint        = false;
            sourceAvatar.CopyToTarget(targetObject);

            Assert.That(sourceAvatar.errors.Count == 0);

            VRCAvatarDescriptor targetVRCAvatarDescriptor = targetObject.GetComponent<VRCAvatarDescriptor>();

            Assert.That(targetVRCAvatarDescriptor.ViewPosition.x, Is.EqualTo(0));
            Assert.That(targetVRCAvatarDescriptor.ViewPosition.y, Is.EqualTo(3));
            Assert.That(targetVRCAvatarDescriptor.ViewPosition.z, Is.EqualTo(3));

            Assert.That(sourceAvatar.messages[0], Is.EqualTo("View Positionをコピー"));

            yield return null;
        }
        [UnityTest]
        public IEnumerator SourceのVRCDescがない場合ViewPositionをコピーできない()
        {
            VRCAvatarDescriptor sourceVRCAvatarDescriptor = (VRCAvatarDescriptor)sourceObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(sourceVRCAvatarDescriptor);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = true;
            sourceAvatar.eyeMovements = true;
            sourceAvatar.baseAnimationLayers = true;
            sourceAvatar.specialAnimationLayers = true;
            sourceAvatar.expressionsMenu = true;
            sourceAvatar.expressionParameters = true;
            sourceAvatar.rotationStates = true;
            sourceAvatar.blueprintId = false;
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

            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(sourceAvatar.errors.Count == 1);
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー元アバターにコンポーネントを追加してください"));

            VRCAvatarDescriptor targetVRCAvatarDescriptor = targetObject.GetComponent<VRCAvatarDescriptor>();

            Assert.That(targetVRCAvatarDescriptor.ViewPosition.x, Is.EqualTo(1));
            Assert.That(targetVRCAvatarDescriptor.ViewPosition.y, Is.EqualTo(1));
            Assert.That(targetVRCAvatarDescriptor.ViewPosition.z, Is.EqualTo(1));

            yield return null;
        }

        [UnityTest]
        public IEnumerator TargetのVRCDescがない場合VRCDescをコピーできない()
        {
            VRCAvatarDescriptor targetVRCAvatarDescriptor = (VRCAvatarDescriptor)targetObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(targetVRCAvatarDescriptor);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = true;
            sourceAvatar.eyeMovements = true;
            sourceAvatar.baseAnimationLayers = true;
            sourceAvatar.specialAnimationLayers = true;
            sourceAvatar.expressionsMenu = true;
            sourceAvatar.expressionParameters = true;
            sourceAvatar.rotationStates = true;
            sourceAvatar.blueprintId = false;
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

            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(sourceAvatar.errors.Count == 1);
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー先アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー先アバターにコンポーネントを追加してください"));

            targetVRCAvatarDescriptor = (VRCAvatarDescriptor)targetObject.GetComponent<VRCAvatarDescriptor>();

            Assert.That(targetVRCAvatarDescriptor == null);

            yield return null;
        }
        [UnityTest]
        public IEnumerator SourceとTargetともにVRCDescがない場合コピーできない()
        {
            VRCAvatarDescriptor sourceVRCAvatarDescriptor = (VRCAvatarDescriptor)sourceObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(sourceVRCAvatarDescriptor);
            VRCAvatarDescriptor targetVRCAvatarDescriptor = (VRCAvatarDescriptor)targetObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(targetVRCAvatarDescriptor);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = true;
            sourceAvatar.eyeMovements = true;
            sourceAvatar.baseAnimationLayers = true;
            sourceAvatar.specialAnimationLayers = true;
            sourceAvatar.expressionsMenu = true;
            sourceAvatar.expressionParameters = true;
            sourceAvatar.rotationStates = true;
            sourceAvatar.blueprintId = false;
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


            Assert.That(sourceAvatar.messages.Count == 0);
            Assert.That(sourceAvatar.errors.Count == 2);
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー元アバターにコンポーネントを追加してください"));
            Assert.That(sourceAvatar.errors[1], Is.EqualTo("コピー先アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー先アバターにコンポーネントを追加してください"));

            targetVRCAvatarDescriptor = (VRCAvatarDescriptor)targetObject.GetComponent<VRCAvatarDescriptor>();
            Assert.That(targetVRCAvatarDescriptor == null);

            sourceVRCAvatarDescriptor = (VRCAvatarDescriptor)sourceObject.GetComponent<VRCAvatarDescriptor>();
            Assert.That(sourceVRCAvatarDescriptor == null);

            yield return null;
        }
        [UnityTest]
        public IEnumerator VRCDescがなくてもphysBonesはコピーできる()
        {
            VRCAvatarDescriptor sourceVRCAvatarDescriptor = (VRCAvatarDescriptor)sourceObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(sourceVRCAvatarDescriptor);
            VRCAvatarDescriptor targetVRCAvatarDescriptor = (VRCAvatarDescriptor)targetObject.GetComponent<VRCAvatarDescriptor>();
            GameObject.DestroyImmediate(targetVRCAvatarDescriptor);

            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = true;
            sourceAvatar.eyeMovements = true;
            sourceAvatar.baseAnimationLayers = true;
            sourceAvatar.specialAnimationLayers = true;
            sourceAvatar.expressionsMenu = true;
            sourceAvatar.expressionParameters = true;
            sourceAvatar.rotationStates = true;
            sourceAvatar.blueprintId = false;
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

            string erroreText = string.Join(", ", sourceAvatar.errors);

            Assert.That(sourceAvatar.errors.Count, Is.EqualTo(2));
            Assert.That(sourceAvatar.errors[0], Is.EqualTo("コピー元アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー元アバターにコンポーネントを追加してください"));
            Assert.That(sourceAvatar.errors[1], Is.EqualTo("コピー先アバターにVRCAvatarDescriptorコンポーネントがないためコピーできませんでした。\n VRCAvatarDescriptor配下の項目のチェックを外すか、コピー先アバターにコンポーネントを追加してください"));
            Assert.That(sourceAvatar.messages.Count, Is.EqualTo(1));
            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Phys Boneをコピー（24件）"));
            yield return null;
        }
    }
}
