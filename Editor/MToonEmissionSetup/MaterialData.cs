using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jirko.Unity.VRoidAvatarUtils.MToonEmissionSetup
{
    public class MaterialData
    {
        public Material material = null;
        public Material sharedMaterial = null;
        public Color defaultLitColor = Color.white;
        public Color defaultShadeColor = Color.white;
        public Color defaultEmissionColor = Color.white;
        public Texture defaultEmissionTexture = null;
    }
}