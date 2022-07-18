using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Jirko.Unity.VRoidAvatarUtils;
using VRC.SDK3.Avatars.Components;
using VRC.Core;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class ObjectTest
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
        public IEnumerator Objectをコピーできる()
        {
            VRoidAvatar sourceAvatar = new VRoidAvatar(sourceObject);
            sourceAvatar.avatarMode = 0;
            sourceAvatar.viewPosition = false;
            sourceAvatar.eyeMovements = false;
            sourceAvatar.baseAnimationLayers = false;
            sourceAvatar.specialAnimationLayers = false;
            sourceAvatar.expressionsMenu = false;
            sourceAvatar.expressionParameters = false;
            sourceAvatar.rotationStates = false;
            sourceAvatar.blueprintId = false;
            sourceAvatar.physBones = false;
            sourceAvatar.physBones_hair = false;
            sourceAvatar.physBones_skirt = false;
            sourceAvatar.physBones_bust = false;
            sourceAvatar.physBones_sleeve = false;
            sourceAvatar.physBones_other = false;
            sourceAvatar.physBoneColiders = false;
            sourceAvatar.objects = true;
            sourceAvatar.aimConstraint = false;
            sourceAvatar.lookAtConstraint = false;
            sourceAvatar.parentConstraint = false;
            sourceAvatar.positionConstraint = false;
            sourceAvatar.rotationConstraint = false;
            sourceAvatar.scaleConstraint = false;
            sourceAvatar.CopyToTarget(targetObject);

            IEnumerable<Transform> children = TestUtil.EnumChildrenRecursive(targetObject.transform);
            Transform[] childrenArray = children.ToArray();

            Assert.That(childrenArray.Length, Is.EqualTo(151));

            Assert.That(sourceAvatar.messages[0], Is.EqualTo("Objectをコピー（1件）"));

            yield return null;
        }

    }
}
