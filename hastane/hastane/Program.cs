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

    /// <summary>
    /// doktor ve hasta bilgilerini tuttuğumuz listelerin bulunduğu bir sınıf
    /// </summary>
    public static class doktorHasta
    {
        public static IList<Doktor> doktor { get; set; }
        public static IList<hasta> hastalar { get; set; }

    }
    /// <summary>
    /// programın ana sınıfı 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// main metodu program çalıştığında ilk çalışan yerdir.
        /// doktor  ve hasta bilgilerini bu metod içinde tanımlıyor
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Doktor> doktorlar = new List<Doktor>();

            poliklinik dahiliye1 = new dahiliye(1);
            poliklinik ortopedi1 = new ortopedi(1);
            kardiyoloji kardiyoloji1 = new kardiyoloji(1);
            // doktornesneleri oluşturup yeni doktorlar ekliyor
            Doktor doktor1 = (new Doktor("ömer faruk küpeli", dahiliye1));
            Doktor doktor2 = (new Doktor("eray güven", ortopedi1));
            Doktor doktor3 = new Doktor("tahir bekem", kardiyoloji1);

            // hasta nesneleri oluşturup hastalara sıra verip hasta kayıtları oluşturuyor
            hasta hasta1 = new hasta(11, "aykut", "kara", dahiliye1, dahiliye1.sıra_al(), doktor1.isim);
            hasta hasta2 = new hasta(12, "sezgin", "sevinç", dahiliye1, dahiliye1.sıra_al(), doktor1.isim);
            hasta hasta3 = new hasta(14, "ömer", "kayaoğlu", ortopedi1, ortopedi1.sıra_al(), doktor2.isim);
            hasta hasta4 = new hasta(15, "ismail", "güven", kardiyoloji1, kardiyoloji1.sıra_al(), doktor3.isim);
            hasta hasta5 = new hasta(12, "kazım", "koçyiğit", dahiliye1, dahiliye1.sıra_al(), doktor1.isim);
            hasta hasta6 = new hasta(12, "ali kaan", "öztürk", ortopedi1, ortopedi1.sıra_al(), doktor2.isim);




            // doktar ve hasta listesine oluşturulan değerler ekleniyor.
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

            //kaydet fonksiyonu çalıştırılıyor.
            //kaydedilen yol başka bilgisayarda değiştirilmeden çalıştırıldığında hata vereceği için yorum satırı haline getirdim.
            //kaydet();


            Console.ReadLine();
        }

        /// <summary>
        /// bu fonksyon doktorHasta sınıfından doktor ve hasta verilerini ayrı ayrı bir json dosyası halinde kaydedilmesini sağlar.
        /// </summary>
        public static void kaydet()
        {

            string json = JsonConvert.SerializeObject(doktorHasta.doktor);
            string json2 = JsonConvert.SerializeObject(doktorHasta.hastalar);

            string yol = @"C:\Users\User\source\repos\hastane\hasta.json";

            using (var tw = new StreamWriter(yol, true))
            {
                tw.WriteLine(json2.ToString());

                tw.Close();
            }
            string yol2 = @"C:\Users\User\source\repos\hastane\doktor.json";
            using (var tw = new StreamWriter(yol2, true))
            {
                tw.WriteLine(json2.ToString());

                tw.Close();
            }



        }



    }
    /// <summary>
    /// doktorların özelliklerinin belirlendiği ana doktor sınıfı
    /// doktorlara ait isim ve poliklinik bilgisi tutar
    /// </summary>
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
    /// <summary>
    /// hastalar için gerekli alanların tanımlandığı kısımdır.
    /// hastalar için tc,sıra,ad,soyad,polikllinik ve doktor bilgileri ister
    /// </summary>
    public class hasta
    {
        public int tc { get; set; }
        public int sira { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public poliklinik hasta_pol { get; set; }
        public string dok { get; set; }

        /// <summary>
        /// hasta sınıfının kurucu metodudur ve dışardan gelen bilgileri kontrol eder.
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="ad"></param>
        /// <param name="soyad"></param>
        /// <param name="hasta_pol"></param>
        /// <param name="sira"></param>
        /// <param name="dok"></param>
        public hasta(int tc, string ad, string soyad, poliklinik hasta_pol, int sira, string dok)
        {
            this.tc = tc;
            this.sira = sira;
            this.ad = ad;
            this.soyad = soyad;
            this.hasta_pol = hasta_pol;
            this.dok = dok;

            // sıra alan hastaları ekrana yazdırır.
            Console.WriteLine("hasta ad soyad : {0} {1} , hasta sıra : {2} , poliklinik : {3} , doktor : {4} ", ad, soyad, sira, hasta_pol, dok);
            Console.WriteLine("________________________________________________________________________________________________________________");
        }

    }
    /// <summary>
    /// poliklinik özelliklerinin ve hangi değişkenleri alacağının belirlendiği ana poliklinik sınıfı
    /// her polikliniğin kendine ait sırası olduğu için güncel sırayı tutmaktadır.
    /// </summary>
    public abstract class poliklinik
    {
        public int sıra { get; set; }
        public abstract int pol_id { get; set; }
        public abstract int sıra_al();
        public abstract int mevcut_sira { get; set; }

    }
    /// <summary>
    /// poliklinik sınıfından miras almıştır dahiliye polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
    public class dahiliye : poliklinik
    {
        public override int pol_id { get; set; }
        public override int mevcut_sira { get; set; }

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
    /// <summary>
    /// poliklinik sınıfından miras almıştır ortopedi polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır kardiyoloji polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır genelcerrahi polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır kadınHastalıklarıVeDoğum polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır cildiye polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır beyinCerrahisi polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır gözHastalıkları polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır plastikCerrahi polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır kulakBurunBoğaz polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
    /// <summary>
    /// poliklinik sınıfından miras almıştır ağızVeDişSağlığı polikliniği için kullanılan değerleri ve mevcut sırayı tutmaktadır.
    /// </summary>
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
