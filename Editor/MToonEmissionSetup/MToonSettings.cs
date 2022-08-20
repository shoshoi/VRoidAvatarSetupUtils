using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace Jirno.MToonEmissionSetup.MToonEmissionSetup
{
    /// <summary>
    /// マテリアルの設定をコピーするやつの設定
    /// </summary>
    [XmlRoot("Settings")]
    public class MToonSettings
    {

        [XmlArray("list")]
        [XmlArrayItem("presets")]
        public List<Preset> presets;

        public class Preset
        {
            [XmlElement("name")]
            public String name;

            /// <summary>
            /// 添え字のプロパティをコピーするか？
            /// </summary>
            [XmlElement("litColor")]
            public Color litColor = Color.white;

            [XmlElement("shadeColor")]
            public Color shadeColor = Color.white;

            [XmlElement("emissionColor")]
            public Color emissionColor = Color.white;

            [XmlElement("emissionTextureType")]
            public int emissionTextureType = 0;

            [XmlElement("SourceTexturePath")]
            public string sourceTexturePath = "";
        }

        /// <summary>
        /// 設定ファイルの保存先
        /// </summary>
        private const string PATH = "Assets/VRoidAvatarSetupUtils/Data/MToonPresetSettings.xml";

        /// <summary>
        /// デフォルトのコンストラクタ（デシリアライザ用）
        /// </summary>
        public MToonSettings()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isCopyProperty">添え字のプロパティをコピーするか？</param>

        /// <summary>
        /// 設定の読込関数
        /// </summary>
        public static MToonSettings load()
        {
            MToonSettings settings;
            try
            {
                using (var fs = new FileStream(PATH, FileMode.Open))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(MToonSettings));
                    settings = (MToonSettings)serializer.Deserialize(fs);
                }

            }
            catch(FileNotFoundException e)
            {
                Debug.Log(e);
                Debug.Log("Settings.xmlがありません");
                settings = new MToonSettings();
                settings.presets = new List<Preset>();
            }

            return settings;
        }

        /// <summary>
        /// 設定の保存関数
        /// </summary>
        private void save()
        {
            using (var fs = new FileStream(PATH, FileMode.Create))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MToonSettings));
                serializer.Serialize(fs, this);
            }
        }
        public void saveAs(Preset preset)
        {
            presets.Add(preset);
            save();
        }
        public void update()
        {
            save();
        }
    }
}
