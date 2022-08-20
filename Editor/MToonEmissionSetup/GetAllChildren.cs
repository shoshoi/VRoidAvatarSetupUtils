using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Jirko.Unity.VRoidAvatarUtils.MToonEmissionSetup
{
	public static class GetAllMaterials
	{
		/// <summary>
		/// GameObject配下の全てのMaterialを取得する関数
		/// </summary>
		/// <param name="obj">対象GameObject</param>
		public static List<MaterialData> GetAll(this GameObject obj)
		{
			List<MaterialData> allMaterials = new List<MaterialData>();
			GetChildren(obj, ref allMaterials);
			return allMaterials;
		}

		/// <summary>
		/// GameObject配下の全てのMaterialを取得する関数
		/// </summary>
		/// <param name="obj">対象GameObject</param>
		/// <param name="allMaterials">Materialのリスト</param>
		public static void GetChildren(GameObject obj, ref List<MaterialData> allMaterials)
		{
			Renderer renderer = obj.GetComponent<Renderer>();
			if (renderer != null)
            {
				for(int i=0; i < renderer.sharedMaterials.Length; i++)
				{
					MaterialData matdata = new MaterialData();
					matdata.material = renderer.sharedMaterials[i];
					matdata.defaultLitColor = renderer.sharedMaterials[i].GetColor("_Color");
					matdata.defaultShadeColor = renderer.sharedMaterials[i].GetColor("_ShadeColor");
					matdata.defaultEmissionColor = renderer.sharedMaterials[i].GetColor("_EmissionColor");
					matdata.defaultEmissionTexture = renderer.sharedMaterials[i].GetTexture("_EmissionMap");

					allMaterials.Add(matdata);
                }
            }

			Transform children = obj.GetComponentInChildren<Transform>();

			//子要素がいなければ終了
			if (children.childCount == 0)
			{
				return;
			}
			foreach (Transform ob in children)
			{
				GetChildren(ob.gameObject, ref allMaterials);
			}
		}
	}
}