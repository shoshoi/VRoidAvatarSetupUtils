using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace Jirko.Unity.VRoidAvatarUtils.MToonEmissionSetup
{
    /// <summary>
    /// VRM/MToonのEmissionの設定をいい感じにするやつ
    /// </summary>
    public class MaterialSetupUtil
    {
        /// <summary>
        /// 設定カラー
        /// </summary>
        private Color litColor = Color.white;
        private Color shadeColor = Color.white;
        private Color emissionColor = Color.white;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="source">コピー元マテリアル</param>
        /// <param name="isCopyProperty">添え字のプロパティをコピーするか？</param>
        public MaterialSetupUtil(Color litColor, Color shadeColor, Color emissionColor)
        {
            this.litColor = litColor;
            this.shadeColor = shadeColor;
            this.emissionColor = emissionColor;
        }

        /// <summary>
        /// マテリアルにカラーを設定する関数
        /// </summary>
        /// <param name="matdata">対象マテリアルデータ</param>
        /// <param name="SourceTexture">Emissionに設定するテクスチャ</param>
        public MaterialData SetUp(MaterialData matdata, Texture SourceTexture)
        {
            if (matdata is null) throw new ArgumentNullException("mat");

            Material material = matdata.material;
            material.SetColor("_Color", litColor);
            material.SetColor("_ShadeColor", shadeColor);
            material.SetColor("_EmissionColor", emissionColor);
            if(SourceTexture != null)
            {
                material.SetTexture("_EmissionMap", SourceTexture);
            }
            else
            {
                material.SetTexture("_EmissionMap", matdata.material.GetTexture("_MainTex"));
            }
            matdata.material = material;
            return matdata;
        }
        /// <summary>
        /// マテリアルにカラーを設定する関数
        /// </summary>
        /// <param name="matdata">対象マテリアルデータ</param>
        public MaterialData Reset(MaterialData matdata)
        {
            if (matdata is null) throw new ArgumentNullException("mat");

            Material material = matdata.material;
            material.SetColor("_Color", matdata.defaultLitColor);
            material.SetColor("_ShadeColor", matdata.defaultShadeColor);
            material.SetColor("_EmissionColor", matdata.defaultEmissionColor);
            material.SetTexture("_EmissionMap", matdata.defaultEmissionTexture);
            matdata.material = material;

            return matdata;
        }
    }
}
