using System.Collections.Generic;

namespace AccountBook
{
    /// <summary>
    /// 类别操作帮助类
    /// </summary>
    public class CategoryHelper
    {
        private List<Category> _data;

        public bool LoadFromFile()
        {
            this._data = IsolatedStorageHelper.ReadObjectFromFile("Category.dat", typeof(List<Category>)) as List<Category>;
            return (this._data != null);
        }

        public bool SaveToFile()
        {
            return IsolatedStorageHelper.WriteObjectToFile("Category.dat", typeof(List<Category>), this._data);
        }

        public List<Category> data
        {
            get
            {
                if (this._data == null)
                {
                    this.LoadFromFile();
                }
                if (this._data == null)
                {
                    this._data = new List<Category>();
                }
                return this._data;
            }
        }
    }
}
