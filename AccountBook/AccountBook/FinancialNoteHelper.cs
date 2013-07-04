using System.Collections.Generic;

namespace AccountBook
{
    public class FinancialNoteHelper
    {
        private List<FinancialNote> _data;

        public bool LoadFromFile()
        {
            this._data = IsolatedStorageHelper.ReadObjectFromFile("FinancialNote.dat", typeof(List<FinancialNote>)) as List<FinancialNote>;
            return (this._data != null);
        }

        public bool SaveToFile()
        {
            return IsolatedStorageHelper.WriteObjectToFile("FinancialNote.dat", typeof(List<FinancialNote>), this._data);
        }

        public List<FinancialNote> data
        {
            get
            {
                if (this._data == null)
                {
                    this.LoadFromFile();
                }
                if (this._data == null)
                {
                    this._data = new List<FinancialNote>();
                }
                return this._data;
            }
        }
    }
}
