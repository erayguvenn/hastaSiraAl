using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace hastane
{


    public class doktorHasta
    {
        public IList <Doktor> doktor { get; set; }
        public IList<hasta> hastalar { get; set; }

    }
    public class Program
    {

        static void Main(string[] args)
        {
            List<Doktor> doktorlar = new List<Doktor>();

            poliklinik dahiliye1 = new dahiliye(1);
            poliklinik ortopedi1 = new ortopedi(1);
            kardiyoloji kardiyoloji1 = new kardiyoloji(1);

            Doktor doktor1 =(new Doktor("ömer faruk küpeli", dahiliye1));
            Doktor doktor2 =(new Doktor("eray güven", ortopedi1));
            Doktor doktor3 = new Doktor("tahir bekem", kardiyoloji1);

            hasta hasta1 = new hasta(11, "aykut", "kara",dahiliye1, dahiliye1.sıra_al(),doktor1.isim);
            hasta hasta2 = new hasta(12, "sezgin", "sevinç",dahiliye1, dahiliye1.sıra_al(),doktor1.isim);
            hasta hasta3 = new hasta(14, "ömer", "kayaoğlu",ortopedi1, ortopedi1.sıra_al(),doktor2.isim);
            hasta hasta4 = new hasta(15, "ismail", "güven", kardiyoloji1, kardiyoloji1.sıra_al(),doktor3.isim);
            hasta hasta5 = new hasta(12, "kazım", "koçyiğit", dahiliye1, dahiliye1.sıra_al(), doktor1.isim);
            hasta hasta6 = new hasta(12, "ali kaan", "öztürk", ortopedi1, ortopedi1.sıra_al(),doktor2.isim);





            doktorHasta doktorHasta = new doktorHasta();
            List<hasta> h = new List<hasta>();
            List<Doktor> d = new List<Doktor>();
            d.Add(doktor1);
            d.Add(doktor2);
            d.Add(doktor3);

            h.Add(hasta1);
            h.Add(hasta2);
            h.Add(hasta3);
            h.Add(hasta4);
            h.Add(hasta5);
            h.Add(hasta6);


            doktorHasta.doktor = d;
            doktorHasta.hastalar = h;

            //string json = JsonConvert.SerializeObject(doktorHasta);
            //string yol = @"D:\D Bilgiler\ders\jsonCevir\hasta.json";

            //using (var tw = new StreamWriter(yol, true))
            //{
            //    tw.WriteLine(json.ToString());
            //    tw.Close();
            //}

            string json = JsonConvert.SerializeObject(doktorHasta);
            string yol = @"D:\D Bilgiler\ders\jsonCevir\hasta.json";

                using (var tw = new StreamWriter(yol, true))
                {
                    tw.WriteLine(json.ToString());
                    tw.Close();
                }
                

            Console.ReadLine();
        }

       

    }

    public class Doktor
    {
        public string isim { get; set; }
        public poliklinik poliklinik { get; set; }

        public Doktor(string isim, poliklinik pol)
        {
            this.isim = isim;
            this.poliklinik = pol;
        }

    }

    public class hasta
    {
        public int tc { get; set; }
        public int sira { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public poliklinik hasta_pol { get; set; }
        public string dok { get; set; }


        public hasta(int tc , string ad,string soyad, poliklinik hasta_pol, int sira,string dok)
        {
            this.tc = tc;
            this.sira = sira;
            this.ad = ad;
            this.soyad = soyad;
            this.hasta_pol = hasta_pol;
            this.dok = dok;

            Console.WriteLine("hasta ad soyad : {0} {1} , hasta sıra : {2} , poliklinik : {3} , doktor : {4} ", ad, soyad, sira,hasta_pol,dok);
            Console.WriteLine("________________________________________________________________________________________________________________");
        }

    }
    
    public abstract class poliklinik
    {
        public int sıra { get; set; }
        public abstract int  pol_id { get; set; }
        public abstract int sıra_al();
        public abstract int mevcut_sira{ get; set; }

    }

    public class dahiliye : poliklinik
    {
        public override int pol_id { get ; set ; }
        public override int mevcut_sira { get ; set ; }

        public dahiliye(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }

        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class ortopedi : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public ortopedi(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class kardiyoloji : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public kardiyoloji(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class genelCerrahi : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public genelCerrahi(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class kadınHastalıklarıVeDoğum : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public kadınHastalıklarıVeDoğum(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class cildiye : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public cildiye(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class beyinCerrahisi : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public beyinCerrahisi(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class gözHastalıkları : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public gözHastalıkları(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class plastikCerrahi : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public plastikCerrahi(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class kulakBurunBoğaz : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public kulakBurunBoğaz(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }
    public class ağızVeDişSağlığı : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }
        public ağızVeDişSağlığı(int id)
        {
            pol_id = id;
            this.mevcut_sira = 1;
        }
        public override int sıra_al()
        {
            return this.mevcut_sira++;
        }
    }




}
