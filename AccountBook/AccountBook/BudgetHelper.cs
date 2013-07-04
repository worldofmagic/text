using System.Collections.Generic;

namespace AccountBook
{
    /// <summary>
    /// 预算操作帮助类
    /// </summary>
    public class BudgetHelper
    {
        private List<Budget> _data;

        public bool LoadFromFile()
        {
            this._data = IsolatedStorageHelper.ReadObjectFromFile("Budget.dat", typeof(List<Budget>)) as List<Budget>;
            return (this._data != null);
        }

        public bool SaveToFile()
        {
            return IsolatedStorageHelper.WriteObjectToFile("Budget.dat", typeof(List<Budget>), this._data);
        }

        public List<Budget> data
        {
            get
            {
                if (this._data == null)
                {
                    this.LoadFromFile();
                }
                if (this._data == null)
                {
                    this._data = new List<Budget>();
                }
                return this._data;
            }
        }
    }
}
