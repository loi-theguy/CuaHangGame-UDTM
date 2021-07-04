﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLDAL
{
    class BLDAL_HoaDon : DataHelper<HoaDon>
    {
        public override int Delete(string pID)
        {
            try
            {
                HoaDon hoaDon = context.HoaDons.FirstOrDefault(hd => hd.MaHD == pID);
                if (hoaDon == null) return NONEXISTENT;
                context.HoaDons.DeleteOnSubmit(hoaDon);
                context.SubmitChanges();
            }
            catch {
                return DEPENDED;
            }
            return SUCCESS;
        }

        public override List<HoaDon> GetData()
        {
            return context.HoaDons.Select(hd=>hd).ToList();
        }

        public override bool Insert(HoaDon entity)
        {
            try
            {
                entity.MaHD = GenerateID();
                context.HoaDons.InsertOnSubmit(entity);
                context.SubmitChanges();
            }
            catch {
                return false;
            }
            return true;
        }

        public override bool Update(HoaDon entity)
        {
            try {
                HoaDon hoaDon = context.HoaDons.FirstOrDefault(hd => hd.MaHD == entity.MaHD);
                hoaDon.MaTK = entity.MaTK;
                hoaDon.NgayLap = entity.NgayLap;
                context.SubmitChanges();
            }
            catch {
                return false;
            }
            return true;
        }

        protected override string GenerateID()
        {
            string type = "HD";
            int max = -1;
            foreach (HoaDon hoaDon in context.HoaDons.Select(hd => hd))
            {
                int temp = int.Parse(hoaDon.MaHD.Substring(2));
                if (temp > max) max = temp;
            }
            max += 1;
            string id = max.ToString();
            while (id.Length < 7)
            {
                id = "0" + id;
            }
            return type + id;
        }
        public List<CTHoaDon> GetDataCTHoaDon(string pMaHD)
        {
            return context.CTHoaDons.Select(ct => ct).Where(ct => ct.MaHD == pMaHD).ToList();
        }
        public bool DeleteCTHoaDon(string pMaHD, string pMaGame)
        {
            try {
                CTHoaDon chiTiet = context.CTHoaDons.FirstOrDefault(ct => ct.MaHD == pMaHD && ct.MaGame == pMaGame);
                context.CTHoaDons.DeleteOnSubmit(chiTiet);
                context.SubmitChanges();
            } catch { return false; }
            return true;
        }
        public bool InsertCTHoaDon(string pMaHD, string pMaGame)
        {
            try
            {
                CTHoaDon chiTiet = new CTHoaDon();
                chiTiet.MaHD = pMaHD;
                chiTiet.MaGame = pMaGame;
                context.CTHoaDons.InsertOnSubmit(chiTiet);
                context.SubmitChanges();
            }
            catch {
                return false;
            }
            return true;
        }
    }
}
