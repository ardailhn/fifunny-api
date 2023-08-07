using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Models
{
    public class Filter
    {

        public int Id { get; set; }
        public int PlaceId { get; set; }

        //genel
        public bool Alkol { get; set; }
        public bool Nargile { get; set; }
        public bool MasaOyunu { get; set; }
        public bool CocukUygun { get; set; }
        public bool CocukOyunAlani { get; set; }

        //fiyat
        public bool fekonomik { get; set; }
        public bool fstandart { get; set; }
        public bool flux { get; set; }
        //yogunluk
        public bool ysakin { get; set; }
        public bool ystandart { get; set; }
        public bool ykalabalik { get; set; }
        //uygunluk
        public bool utoplanti { get; set; }
        public bool udans { get; set; }
        public bool usohbet { get; set; }
        public bool uders { get; set; }
        public bool uyemek { get; set; }
        public bool utatli { get; set; }
        public bool ukahvalti { get; set; }
        //servis turu
        public bool sself { get; set; }
        public bool sgarson { get; set; }
        public bool stake { get; set; }
        //muzik turu
        public bool mrap { get; set; }
        public bool mrock { get; set; }
        public bool mpop { get; set; }
        public bool mhiphop { get; set; }
        public bool mklasik { get; set; }
        public bool mnostaji { get; set; }
        public bool myerli { get; set; }
        public bool myabanci { get; set; }
        //tasarim turu
        public bool tmodern { get; set; }
        public bool tvintage { get; set; }
        public bool tsalas { get; set; }
        public bool totantik { get; set; }
        public bool tbutik { get; set; }
        public bool tkitap { get; set; }
        //masa turu
        public bool mmasa { get; set; }
        public bool mbistro { get; set; }
        public bool mayakta { get; set; }
        public bool mloca { get; set; }



        public Place Place { get; set; }

    }
}