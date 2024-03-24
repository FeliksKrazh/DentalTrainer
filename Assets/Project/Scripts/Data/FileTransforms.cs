using DentalTrainer_FeliksKrazhau;
using System.Collections.Generic;
using UnityEngine;

namespace DataUtil
{
    public class FileTransforms : MonoBehaviour
    {
        [SerializeField] private List<Transform> targetTransforms;
        private DataTransforms dataTransforms = new DataTransforms();

        public List<Transform> TargetTransforms
        {
            get => targetTransforms;
        }

        private void Start()
        {
            Invoke("Load", 0.1f);
        }

        [ContextMenu("Test Save")]
        public void Save()
        {
            dataTransforms.objs.Clear();
            foreach (Transform t in targetTransforms)
            {
                DataTransform dataTransform = new DataTransform();
                dataTransform.pos = t.position;
                dataTransform.rot = t.rotation;
                dataTransform.scale = t.localScale;
                TransformationSwitcher transformationSwitcher = t.GetComponent<TransformationSwitcher>();
                if (transformationSwitcher != null)
                {
                    dataTransform.isParent = transformationSwitcher.IsParent;
                }
                else
                {
                    dataTransform.isParent = false;
                }
                dataTransforms.objs.Add(dataTransform);
            }
            ResourcesDataUtil.SaveData(dataTransforms);
        }

        [ContextMenu("Test Load")]
        public void Load()
        {
            dataTransforms = ResourcesDataUtil.LoadData<DataTransforms>();
            if (dataTransforms != null)
            {
                int i = 0;
                foreach (DataTransform dataT in dataTransforms.objs)
                {
                    targetTransforms[i].position = dataT.pos;
                    targetTransforms[i].rotation = dataT.rot;
                    targetTransforms[i].localScale = dataT.scale;
                    TransformationSwitcher transformationSwitcher = targetTransforms[i].GetComponent<TransformationSwitcher>();
                    if (transformationSwitcher != null)
                    {
                        transformationSwitcher.IsParent = dataT.isParent;
                    }
                    i++;
                }
            }
        }
        [ContextMenu("Test Reset to Parent")]
        public void ResetToParent()
        {
            foreach (Transform t in targetTransforms)
            {
                TransformationSwitcher transformationSwitcher = t.GetComponent<TransformationSwitcher>();
                if (transformationSwitcher != null)
                {
                    transformationSwitcher.IsParent = true;
                }
            }
        }
    }
}
