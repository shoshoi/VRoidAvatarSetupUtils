﻿using UnityEditor;
using UnityEngine;

namespace Jirko.Unity.VRoidAvatarUtils
{
    public class CopyAvatarParametersUI : EditorWindow
    {
        bool folding = true;
        Vector2 parametersScrollPosition = new Vector2(0, 0);
        Vector2 messagesScrollPosition = new Vector2(0, 0);
        public GameObject sourceObject = null;
        public GameObject targetObject = null;

        public int avatarMode = 0;
        public bool viewPosition = true;
        public bool eyeMovements = true;
        public bool baseAnimationLayers = true;
        public bool specialAnimationLayers = true;
        public bool expressionsMenu = true;
        public bool expressionParameters = true;
        public bool rotationStates = true;
        public bool blueprintId = true;
        public bool physBones = true;
        public bool physBones_hair = true;
        public bool physBones_skirt = true;
        public bool physBones_bust = true;
        public bool physBones_sleeve = true;
        public bool physBones_other = true;
        public bool physBoneColiders = true;
        public bool objects = false;

        public bool aimConstraint = true;
        public bool lookAtConstraint = true;
        public bool parentConstraint = true;
        public bool positionConstraint = true;
        public bool rotationConstraint = true;
        public bool scaleConstraint = true;

        public string messages = "";
        public string errors = "";

        private VRoidAvatar sourceAvatarDTO = null;

        [MenuItem("VRoidAvatarSetup/Open CopyAvatarParameters Wizard", priority = 1)]
        static void ShowWindow()
        {
            var window = EditorWindow.GetWindow<CopyAvatarParametersUI>();
            window.minSize = new Vector2(400, 600);
        }

        void OnGUI()
        {
            parametersScrollPosition = EditorGUILayout.BeginScrollView(parametersScrollPosition);
            EditorGUILayout.LabelField("VRC Avatar", EditorStyles.boldLabel);
            sourceObject = (GameObject)EditorGUILayout.ObjectField("Source Avatar", sourceObject, typeof(GameObject), true);
            targetObject = (GameObject)EditorGUILayout.ObjectField("Destination Avatar", targetObject, typeof(GameObject), true);
            avatarMode = EditorGUILayout.IntPopup("Avatar Type", avatarMode, new string[] { "VRoid Avatar", "Other Avatar" }, new int[] { 0, 1 });
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("VRC Avatar Descripter", EditorStyles.boldLabel);

            if (folding = EditorGUILayout.Foldout(folding, "View"))
            {
                viewPosition = EditorGUILayout.Toggle("View Position", viewPosition);
            }

            if (folding = EditorGUILayout.Foldout(folding, "Eye Look"))
            {
                eyeMovements = EditorGUILayout.Toggle("Eye Movements", eyeMovements);
                rotationStates = EditorGUILayout.Toggle("Rotation States", rotationStates);

            }

            if (folding = EditorGUILayout.Foldout(folding, "Playable Layers"))
            {
                baseAnimationLayers = EditorGUILayout.Toggle("Base", baseAnimationLayers);
                specialAnimationLayers = EditorGUILayout.Toggle("Special", specialAnimationLayers);
            }

            if (folding = EditorGUILayout.Foldout(folding, "Expressions"))
            {
                expressionsMenu = EditorGUILayout.Toggle("Menu", expressionsMenu);
                expressionParameters = EditorGUILayout.Toggle("Parameters", expressionParameters);

            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Pipeline Manager", EditorStyles.boldLabel);
            blueprintId = EditorGUILayout.Toggle("Blueprint ID", blueprintId);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Phys Bone", EditorStyles.boldLabel);

            if (avatarMode == 0)
            {
                EditorGUILayout.LabelField("Phys Bones", EditorStyles.boldLabel);
                physBones_hair = EditorGUILayout.Toggle("　髪の毛", physBones_hair);
                physBones_skirt = EditorGUILayout.Toggle("　スカート", physBones_skirt);
                physBones_sleeve = EditorGUILayout.Toggle("　袖", physBones_sleeve);
                physBones_bust = EditorGUILayout.Toggle("　胸", physBones_bust);
                physBones_other = EditorGUILayout.Toggle("　その他", physBones_other);
            }
            else
            {
                physBones = EditorGUILayout.Toggle("Phys Bones", physBones);
            }
            physBoneColiders = EditorGUILayout.Toggle("Phys Bone Coliders", physBoneColiders);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Object", EditorStyles.boldLabel);
            objects = EditorGUILayout.Toggle("Objects", objects);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Constraints", EditorStyles.boldLabel);
            aimConstraint = EditorGUILayout.Toggle("Aim", aimConstraint);
            lookAtConstraint = EditorGUILayout.Toggle("Look At", lookAtConstraint);
            parentConstraint = EditorGUILayout.Toggle("Parent", parentConstraint);
            positionConstraint = EditorGUILayout.Toggle("Position", positionConstraint);
            rotationConstraint = EditorGUILayout.Toggle("Rotation", rotationConstraint);
            scaleConstraint = EditorGUILayout.Toggle("Scale", scaleConstraint);
            EditorGUILayout.Space();

            if (sourceObject == null || targetObject == null)
            {
                EditorGUI.BeginDisabledGroup(true);
            }

            EditorGUILayout.EndScrollView();

            if (GUILayout.Button("Copy"))
            {
                sourceAvatarDTO = new VRoidAvatar(sourceObject);

                sourceAvatarDTO.avatarMode = avatarMode;
                sourceAvatarDTO.viewPosition = viewPosition;
                sourceAvatarDTO.eyeMovements = eyeMovements;
                sourceAvatarDTO.baseAnimationLayers = baseAnimationLayers;
                sourceAvatarDTO.specialAnimationLayers = specialAnimationLayers;
                sourceAvatarDTO.expressionsMenu = expressionsMenu;
                sourceAvatarDTO.expressionParameters = expressionParameters;
                sourceAvatarDTO.rotationStates = rotationStates;
                sourceAvatarDTO.blueprintId = blueprintId;
                sourceAvatarDTO.physBones = physBones;
                sourceAvatarDTO.physBones_hair = physBones_hair;
                sourceAvatarDTO.physBones_skirt = physBones_skirt;
                sourceAvatarDTO.physBones_bust = physBones_bust;
                sourceAvatarDTO.physBones_sleeve = physBones_sleeve;
                sourceAvatarDTO.physBones_other = physBones_other;
                sourceAvatarDTO.physBoneColiders = physBoneColiders;
                sourceAvatarDTO.objects = objects;
                sourceAvatarDTO.aimConstraint = aimConstraint;
                sourceAvatarDTO.lookAtConstraint = lookAtConstraint;
                sourceAvatarDTO.parentConstraint = parentConstraint;
                sourceAvatarDTO.positionConstraint = positionConstraint;
                sourceAvatarDTO.rotationConstraint = rotationConstraint;
                sourceAvatarDTO.scaleConstraint = scaleConstraint;

                Undo.RecordObject(targetObject, "Copy Parameters " + targetObject.name);
                EditorUtility.SetDirty(targetObject);
                messages = "";
                errors = "";
                sourceAvatarDTO.CopyToTarget(targetObject);
                if (sourceAvatarDTO.errors.Count > 0)
                {
                    errors = "---エラー--------------\n";
                    int errorcount = 1;
                    foreach (string error in sourceAvatarDTO.errors)
                    {
                        errors = errors + errorcount.ToString() + "." + error + "\n";
                        errorcount += 1;
                    }
                }

                if (sourceAvatarDTO.messages.Count > 0)
                {
                    messages = "---メッセージ-----------\n";
                    int messagecount = 1;
                    foreach (string message in sourceAvatarDTO.messages)
                    {
                        messages = messages + messagecount.ToString() + "." + message + "\n";
                        messagecount += 1;
                    }
                    messages = messages + "コピーが完了しました。";
                }
                if (errors != "")
                {
                    messages = errors + messages;
                }
            }
            if (sourceObject == null || targetObject == null)
            {
                EditorGUI.EndDisabledGroup();
            }
            messagesScrollPosition = EditorGUILayout.BeginScrollView(messagesScrollPosition);

            GUILayout.Label(messages);
            EditorGUILayout.EndScrollView();
        }
    }
}