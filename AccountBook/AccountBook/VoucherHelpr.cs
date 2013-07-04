using System;
using System.Collections.Generic;
namespace AccountBook
{
    public class VoucherHelpr
    {
        //记账列表
        private List<Voucher> _data;
        // 初始化
        public VoucherHelpr()
        {
            if (!this.LoadFromFile())
            {
                this._data = new List<Voucher>();
            }
        }
        // 添加一条记账记录
        public void AddNew(Voucher item)
        {
            item.ID = Guid.NewGuid();
            this.data.Add(item);
        }
        // 读取记账列表
        public bool LoadFromFile()
        {
            this._data = IsolatedStorageHelper.ReadObjectFromFile("Voucher.dat", typeof(List<Voucher>)) as List<Voucher>;
            return (this._data != null);
        }
        // 移除一条记录
        public void Remove(Voucher item)
        {
            this.data.Remove(item);
        }
        // 保存记账列表
        public bool SaveToFile()
        {
            return IsolatedStorageHelper.WriteObjectToFile("Voucher.dat", typeof(List<Voucher>), this._data);
        }
        // 获取记账列表
        public List<Voucher> data
        {
            get
            {
                if (this._data == null)
                {
                    this.LoadFromFile();
                }
                if (this._data == null)
                {
                    this._data = new List<Voucher>();
                }
                return this._data;
            }
        }
    }
}
