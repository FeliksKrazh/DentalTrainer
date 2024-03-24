using DataUtil;
using System.Collections.Generic;
using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class InfoSystem : MonoBehaviour
    {
        [SerializeField] private GameObject helpPanel;
        [SerializeField] private GameObject infoObjectPrefab;
        [SerializeField] private FileTransforms fileTransforms;
        [TextArea(2, 4)]
        [SerializeField] private string[] names;
        private List<InfoObject> infoObjects = new List<InfoObject>();
        private bool isVisible = false;

        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                if (helpPanel != null)
                {
                    helpPanel.SetActive(isVisible);
                }
                foreach (InfoObject infoObject in infoObjects)
                {
                    if (infoObject != null)
                    {
                        infoObject.SetVisible(isVisible);
                    }
                }
            }
        }
        private void Start()
        {
            int i = 0;
            foreach (Transform t in fileTransforms.TargetTransforms)
            {
                try
                {
                    if (t.GetComponent<Camera>() == null)
                    {
                        GameObject obj = Instantiate(infoObjectPrefab, t);
                        InfoObject infoObject = obj.GetComponent<InfoObject>();
                        obj.name = infoObjectPrefab.name + "_" + i.ToString();
                        if (infoObject != null)
                        {
                            WorldToScreenConvert worldToScreenConvert = obj.GetComponentInChildren<WorldToScreenConvert>();
                            if (worldToScreenConvert != null)
                            {
                                worldToScreenConvert.TargetTransform = t;
                            }
                            infoObject.Text = names[i];
                            infoObject.SetVisible(isVisible);
                            infoObjects.Add(infoObject);
                        }
                        else
                        {
                            Destroy(obj);
                        }
                    }
                }
                catch
                {
                    print(t.name + " no InfoObject component");
                }
                i++;
            }
        }
    }
}
