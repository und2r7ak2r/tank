using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Unity.Mathematics;
using UnityEngine;

namespace ShootingEditor2D
{
    public class LevelEditor : MonoBehaviour
    {
        public SpriteRenderer EmptyHighlight;
        private GameObject mCurrentObjectMouseOn;
        private bool mCanDraw;
        public enum OperateMode
        {
            Draw,
            Erase
        }
        public enum BrushType
        {
            Ground,
            Hero
        }

        private OperateMode mCurrentOperateMode = OperateMode.Draw;
        private BrushType mCurrentBrushType = BrushType.Ground;

        private readonly Lazy<GUIStyle> mModeLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 30,
            alignment = TextAnchor.MiddleCenter
        });

        private readonly Lazy<GUIStyle> mButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 30,
        });
        private readonly Lazy<GUIStyle> mRightButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button) 
        {
            fontSize = 25
        });


        private void OnGUI()
        {
            var modeLabelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, 35, 200, 50);
            if (mCurrentOperateMode == OperateMode.Draw) 
            {
                GUI.Label(modeLabelRect, mCurrentOperateMode + ":" + mCurrentBrushType, mModeLabelStyle.Value);
            }
            else
            {
                GUI.Label(modeLabelRect, mCurrentOperateMode.ToString(), mModeLabelStyle.Value);
            }

            var drawButtonRect = new Rect(10, 10, 150, 40);
            if (GUI.Button(drawButtonRect, "绘制", mButtonStyle.Value))
            {
                mCurrentOperateMode = OperateMode.Draw;
            }

            var eraseButtonRect = new Rect(10, 60, 150, 40);
            if (GUI.Button(eraseButtonRect, "橡皮", mButtonStyle.Value))
            {
                mCurrentOperateMode = OperateMode.Erase;
            }

            if (mCurrentOperateMode == OperateMode.Draw) 
            {
                var groundButtonRect = new Rect(Screen.width - 110, 10, 100, 40); 
                if (GUI.Button(groundButtonRect, "地块", mRightButtonStyle.Value)) 
                {
                    mCurrentBrushType = BrushType.Ground;
                }

                var heroButtonRect = new Rect(Screen.width - 110, 60, 100, 40); 
                if (GUI.Button(heroButtonRect, "主角", mRightButtonStyle.Value)) 
                {
                    mCurrentBrushType = BrushType.Hero;
                }
            }
            var saveButtonRect = new Rect(Screen.width - 110, Screen.height - 50, 100, 40); 
            if (GUI.Button(saveButtonRect, "保存", mRightButtonStyle.Value)) 
            {
                List<LevelItemInfo> infos= new List<LevelItemInfo>(transform.childCount);
                foreach(Transform child in transform)
                {
                    infos.Add(new LevelItemInfo()
                    {
                        X= child.position.x,
                        Y= child.position.y,
                        Name= child.name
                    });
                }
                XmlDocument document = new XmlDocument();
                var declaration = document.CreateXmlDeclaration("1.0", "UTF-8","");
                document.AppendChild(declaration);
                var level = document.CreateElement("Level");
                document.AppendChild(level);
                foreach(var levelItemInfo in infos)
                {
                    var levelItem = document.CreateElement("LevelItem");
                    levelItem.SetAttribute("name", levelItemInfo.Name);
                    levelItem.SetAttribute("x",levelItemInfo.X.ToString());
                    levelItem.SetAttribute("y",levelItemInfo.Y.ToString());
                    level.AppendChild(levelItem);
                }
                var levelFilesFolder = Application.persistentDataPath + "/LevelFiles";
                Debug.Log(levelFilesFolder);
                if (!Directory.Exists(levelFilesFolder))
                {
                    Directory.CreateDirectory(levelFilesFolder);
                }
                var levelFilePath = levelFilesFolder + "/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
                document.Save(levelFilePath);
            }

        }
        class LevelItemInfo
        {
            public float X;
            public float Y;
            public string Name;
        }



        private void Update()
        {
            var mousePosition = Input.mousePosition;
            var worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);
            worldMousePos.x = math.floor(worldMousePos.x + 0.5f);
            worldMousePos.y = math.floor(worldMousePos.y + 0.5f);
            worldMousePos.z = 0;
            if (GUIUtility.hotControl == 0)
            {
                EmptyHighlight.gameObject.SetActive(true);
            }
            else
            {
                EmptyHighlight.gameObject.SetActive(false);
            }
            if (math.abs(EmptyHighlight.transform.position.x - worldMousePos.x) < 0.1f &&
                math.abs(EmptyHighlight.transform.position.y - worldMousePos.y) < 0.1f)
            {

            }
            else
            {
                var emptyHighlightPos = worldMousePos;
                emptyHighlightPos.z = -1;
                EmptyHighlight.transform.position = emptyHighlightPos;
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                var hit = Physics2D.Raycast(ray.origin, Vector2.zero, Mathf.Infinity);
                if (hit.collider)
                {
                    if (mCurrentOperateMode == OperateMode.Draw)
                    {
                        EmptyHighlight.color = new Color(1, 0, 0, 0.5f);
                    }
                    else
                    {
                        EmptyHighlight.color = new Color(1, 0.5f, 0, 0.5f);
                    }

                    mCanDraw = false;
                    mCurrentObjectMouseOn = hit.collider.gameObject;
                }
                else
                {
                    if (mCurrentOperateMode == OperateMode.Draw)
                    {
                        EmptyHighlight.color = new Color(1, 1, 1, 0.5f);
                    }
                    else
                    {
                        EmptyHighlight.color = new Color(0, 0, 1, 0.5f);
                    }
                    mCanDraw = true;
                    mCurrentObjectMouseOn = null;
                }
            }
            if ((Input.GetMouseButtonDown(0)|| Input.GetMouseButton(0) )&& GUIUtility.hotControl == 0)
            {
                if (mCanDraw && mCurrentOperateMode == OperateMode.Draw)
                {
                    if (mCurrentBrushType == BrushType.Ground) 
                    {
                        var groundPrefab = Resources.Load<GameObject>("Ground");
                        var groundGameObj = Instantiate(groundPrefab, transform);
                        groundGameObj.transform.position = worldMousePos;
                        groundGameObj.name = "Ground";

                        mCanDraw = false;
                    }
                    else if (mCurrentBrushType == BrushType.Hero)
                    {
                        var groundPrefab = Resources.Load<GameObject>("Ground");
                        var groundGameObj = Instantiate(groundPrefab, transform);
                        groundGameObj.transform.position = worldMousePos;
                        groundGameObj.name = "Player";

                        groundGameObj.GetComponent<SpriteRenderer>().color = Color.cyan;

                        mCanDraw = false;
                    }
                }
                else if (mCurrentObjectMouseOn && mCurrentOperateMode == OperateMode.Erase)
                {
                    Destroy(mCurrentObjectMouseOn);

                    mCurrentObjectMouseOn = null;
                }


            }
        }
    }
}