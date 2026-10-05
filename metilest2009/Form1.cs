using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace metilest2009
{
    public partial class InsermentoDati : Form
    {
        public InsermentoDati()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float C4, C6, C8, C10, C12, C14, C14m, C15, C16ISO, C16, C16m, C17ISO, C17, C17m,
                C18, C18ISO, C18m, C18mm, C18mmm, C18CON, C20, C20m, C22, C22m;
            int PalmaRafMin, PalmaRafMax, PalmaAfrMin, PalmaAfrMax,
                PalmaOle60Min, PalmaOle60Max, PalmaOle62Min, PalmaOle62Max, PalmaOle64Min, PalmaOle64Max,
                PalmaSt48Min, PalmaSt48Max, PalmaSt53Min, PalmaSt53Max, CoccoRafMin, CoccoRafMax, CoccoIdrMin,
                CoccoIdrMax, PalmistoRMin, PalmistoRMax, PalmistoIMin, PalmistoIMax, PalmistoStMin,
                PalmistoStMax, PalmistoFrazMin, PalmistoFrazMax, SoiaRafMin, SoiaRafMax, ColzaRafMin, ColzaRafMax,
                ArachideRMin, ArachideRMax, VinaccioloMin, VinaccioloMax, MaisRafMin, MaisRafMax,
                GirasoleALinoMin, GirasoleALinoMax, GirasoleAOleMin, GirasoleAOleMax, SesamoRaffMin, SesamoRaffMax,
                NocciolaMin, NocciolaMax, OlivaMin, OlivaMax, BurroCacaoMin, BurroCacaoMax, BabassuMin, BabassuMax,
                KariteMin, KariteMax, BurroMin, BurroMax, StruttoMin, StruttoMax, SegoRafMin, SegoRafMax;

            float.TryParse(txtC4.Text, out C4);
            float.TryParse(txtC6.Text, out C6);
            float.TryParse(txtC8.Text, out C8);
            float.TryParse(txtC10.Text, out C10);
            float.TryParse(txtC12.Text, out C12);
            float.TryParse(txtC14.Text, out C14);
            float.TryParse(txtC14m.Text, out C14m);
            float.TryParse(txtC15.Text, out C15);
            float.TryParse(txtC16ISO.Text, out C16ISO);
            float.TryParse(txtC16.Text, out C16);
            float.TryParse(txtC16m.Text, out C16m);
            float.TryParse(txtC17ISO.Text, out C17ISO);
            float.TryParse(txtC17.Text, out C17);
            float.TryParse(txtC17m.Text, out C17m);
            float.TryParse(txtC18.Text, out C18);
            float.TryParse(txtC18ISO.Text, out C18ISO);
            float.TryParse(txtC18m.Text, out C18m);
            float.TryParse(txtC18mm.Text, out C18mm);
            float.TryParse(txtC18mmm.Text, out C18mmm);
            float.TryParse(txtC18CON.Text, out C18CON);
            float.TryParse(txtC20.Text, out C20);
            float.TryParse(txtC20m.Text, out C20m);
            float.TryParse(txtC22.Text, out C22);
            float.TryParse(txtC22m.Text, out C22m);
           
            int.TryParse(txtPalmaRafMin.Text,out PalmaRafMin);
            int.TryParse(txtPalmaRafMax.Text, out PalmaRafMax);
            //MessageBox.Show(PalmaRafMax.ToString());
            int.TryParse(txtPalmaAfrMin.Text, out PalmaAfrMin);
            int.TryParse(txtPalmaAfrMax.Text, out PalmaAfrMax);
            int.TryParse(txtPalmaOle60Min.Text, out PalmaOle60Min);
            int.TryParse(txtPalmaOle60Max.Text, out PalmaOle60Max);
            int.TryParse(txtPalmaOle62Min.Text, out PalmaOle62Min);
            int.TryParse(txtPalmaOle62Max.Text, out PalmaOle62Max);
            int.TryParse(txtPalmaOle64Min.Text, out PalmaOle64Min);
            int.TryParse(txtPalmaOle64Max.Text, out PalmaOle64Max);
            int.TryParse(txtPalmaSte48Min.Text, out PalmaSt48Min);
            int.TryParse(txtPalmaSte48Max.Text, out PalmaSt48Max);
            int.TryParse(txtPalmaSte53Min.Text, out PalmaSt53Min);
            int.TryParse(txtPalmaSte53Max.Text, out PalmaSt53Max);
            int.TryParse(txtCoccoRafMin.Text, out CoccoRafMin);
            int.TryParse(txtCoccoRafMax.Text, out CoccoRafMax);
            int.TryParse(txtCoccoIdMin.Text, out CoccoIdrMin);
            int.TryParse(txtCoccoIdMax.Text, out CoccoIdrMax);
            int.TryParse(txtPalmistiRafMin.Text, out PalmistoRMin);
            int.TryParse(txtPalmistiRafMax.Text, out PalmistoRMax);
            int.TryParse(txtPalmistiIdMin.Text, out PalmistoIMin);
            int.TryParse(txtPalmistiIdMax.Text, out PalmistoIMax);
            int.TryParse(txtPalmistiOleMin.Text, out PalmistoStMin);
            int.TryParse(txtPalmistiOleMax.Text, out PalmistoStMax);
            int.TryParse(txtPalmistiFrazMin.Text, out PalmistoFrazMin);
            int.TryParse(txtPalmistiFrazMax.Text, out PalmistoFrazMax);
            int.TryParse(txtSoiaRafMin.Text, out SoiaRafMin);
            int.TryParse(txtSoiaRafMax.Text, out SoiaRafMax);
            int.TryParse(txtColzaRafMin.Text, out ColzaRafMin);
            int.TryParse(txtColzaRafMax.Text, out ColzaRafMax);
            int.TryParse(txtArachideRafMin.Text, out ArachideRMin);
            int.TryParse(txtArachideRafMax.Text, out ArachideRMax);
            int.TryParse(txtVinaccioloMin.Text, out VinaccioloMin);
            int.TryParse(txtVinaccioloMax.Text, out VinaccioloMax);
            int.TryParse(txtMaisRafMin.Text, out MaisRafMin);
            int.TryParse(txtMaisRafMax.Text, out MaisRafMax);
            int.TryParse(txtGirasoleALinoleicoMin.Text, out GirasoleALinoMin);
            int.TryParse(txtGirasoleALinoleicoMax.Text, out GirasoleALinoMax);
            int.TryParse(txtGirasoleAOleicoMin.Text, out GirasoleAOleMin);
            int.TryParse(txtGirasoleAOleicoMax.Text, out GirasoleAOleMax);
            int.TryParse(txtSesamoRaffMin.Text, out SesamoRaffMin);
            int.TryParse(txtSesamoRaffMax.Text, out SesamoRaffMax);
            int.TryParse(txtNocciolaMin.Text, out NocciolaMin);
            int.TryParse(txtNocciolaMax.Text, out NocciolaMax);
            int.TryParse(txtOlivaMin.Text, out OlivaMin);
            int.TryParse(txtOlivaMax.Text, out OlivaMax);
            int.TryParse(txtBurroCacaoMin.Text, out BurroCacaoMin);
            int.TryParse(txtBurroCacaoMax.Text, out BurroCacaoMax);
            int.TryParse(txtBabassuMin.Text, out BabassuMin);
            int.TryParse(txtBabassuMax.Text, out BabassuMax);
            int.TryParse(txtKariteMin.Text, out KariteMin);
            int.TryParse(txtKariteMax.Text, out KariteMax);
            int.TryParse(txtBurroMin.Text, out BurroMin);
            int.TryParse(txtBurroMax.Text, out BurroMax);
            int.TryParse(txtStruttoMin.Text, out StruttoMin);
            int.TryParse(txtStruttoMax.Text, out StruttoMax);
            int.TryParse(txtSegoRafMin.Text, out SegoRafMin);
            int.TryParse(txtSegoRafMax.Text, out SegoRafMax);
            
            //MessageBox.Show(txtPalmaRafMax.Text + "-" + PalmaRafMax.ToString());
            Form FormElaborazione = new Elaborazione(Convert.ToInt32(stepperc.Text),
                C4, C6, C8, C10, C12, C14, C14m, C15, C16ISO, C16, C16m, C17ISO, C17, C17m,
                C18, C18ISO, C18m, C18mm, C18mmm, C18CON, C20, C20m, C22, C22m,
                PalmaRafMin, PalmaRafMax, PalmaAfrMin, PalmaAfrMax,
                PalmaOle60Min, PalmaOle60Max, PalmaOle62Min, PalmaOle62Max, PalmaOle64Min, PalmaOle64Max,
                PalmaSt48Min, PalmaSt48Max, PalmaSt53Min, PalmaSt53Max, CoccoRafMin, CoccoRafMax, CoccoIdrMin,
                CoccoIdrMax, PalmistoRMin, PalmistoRMax, PalmistoIMin, PalmistoIMax, PalmistoStMin,
                PalmistoStMax, PalmistoFrazMin, PalmistoFrazMax, SoiaRafMin, SoiaRafMax, ColzaRafMin, ColzaRafMax,
                ArachideRMin, ArachideRMax, VinaccioloMin, VinaccioloMax, MaisRafMin, MaisRafMax,
                GirasoleALinoMin, GirasoleALinoMax, GirasoleAOleMin, GirasoleAOleMax, SesamoRaffMin, SesamoRaffMax,
                NocciolaMin, NocciolaMax, OlivaMin, OlivaMax, BurroCacaoMin, BurroCacaoMax, BabassuMin, BabassuMax,
                KariteMin, KariteMax, BurroMin, BurroMax, StruttoMin, StruttoMax, SegoRafMin, SegoRafMax);
            FormElaborazione.Show();
        }


        private void InsermentoDati_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}