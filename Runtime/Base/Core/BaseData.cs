using UnityEngine;

namespace JFramework.Basic
{
    public class BaseData : ScriptableObject, IData
    {
        public virtual void InitData()
        {
        }

        public virtual void SaveData()
        {
        }

        public virtual void LoadData()
        {
        }
    }
}