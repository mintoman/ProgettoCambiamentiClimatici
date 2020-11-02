using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace UnityEngine.Tilemaps
{
    public class FlowerTile : Tile
    {
        [SerializeField]
        public Sprite[] m_Sprites;

        [SerializeField]
        public Sprite m_Preview;

        public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
        {
            var floor2 = 150.0f;
            /*var floor3 = 30.0f;
            var floor4 = 32.0f;
            var floor5 = 34.0f;
            var floor6 = 36.0f;
            var floor7 = 38.0f;*/

            var choosen = Random.Range(0.0f, 300.0f);

            if (choosen >= 0f && choosen < floor2)
            {
                tileData.sprite = m_Sprites[1];
            }
            /*else if (choosen >= floor2 && choosen < floor3)
            {
                tileData.sprite = prisonSprites[2];
            }
            else if (choosen >= floor3 && choosen < floor4)
            {
                tileData.sprite = prisonSprites[3];
            }
            else if (choosen >= floor4 && choosen < floor5)
            {
                tileData.sprite = prisonSprites[4];
            }
            else if (choosen >= floor5 && choosen < floor6)
            {
                tileData.sprite = prisonSprites[5];
            }
            else if (choosen >= floor6 && choosen <= floor7)
            {
                tileData.sprite = prisonSprites[6];
            }*/
            else
            {
                tileData.sprite = m_Sprites[0];
            }

        }

#if UNITY_EDITOR
        [MenuItem("Assets/FlowerTile")]
        public static void CreateFlowerTile()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Flowertile", "New FlowerTile", "asset", "Save flower tile", "Assets");
            if (path == "")
            {
                return;
            }

            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<FlowerTile>(), path);
        }
#endif
    }
}
