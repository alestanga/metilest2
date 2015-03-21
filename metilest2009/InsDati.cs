/*
  Copyright 2009-2015 Alessandro Stanga
  This file is part of metilest2.

   metilest2 is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   metilest2 is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with metilest2.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace metilest2009
{
    public partial class InsermentoDati : Form
    {
        //costanti
        public const int NUM_ACIDI_GRASSI = 24; //totale acidi grassi analizzati
        //standard
        private float[] stdPalmaRaf = new float[NUM_ACIDI_GRASSI], stdPalmaAfr = new float[NUM_ACIDI_GRASSI], stdPalmaOle60 = new float[NUM_ACIDI_GRASSI], stdPalmaOle62 = new float[NUM_ACIDI_GRASSI],
            stdPalmaOle64 = new float[NUM_ACIDI_GRASSI], stdPalmaSt48 = new float[NUM_ACIDI_GRASSI], stdPalmaSt53 = new float[NUM_ACIDI_GRASSI], stdCoccoRaf = new float[NUM_ACIDI_GRASSI], stdCoccoIdr = new float[NUM_ACIDI_GRASSI],
            stdPalmistoR = new float[NUM_ACIDI_GRASSI], stdPalmistoI = new float[NUM_ACIDI_GRASSI], stdPalmistoSt = new float[NUM_ACIDI_GRASSI], stdPalmistoFraz = new float[NUM_ACIDI_GRASSI], stdSoiaRaf = new float[NUM_ACIDI_GRASSI],
            stdColzaRaf = new float[NUM_ACIDI_GRASSI], stdArachideR = new float[NUM_ACIDI_GRASSI], stdVinacciolo = new float[NUM_ACIDI_GRASSI], stdMaisRaf = new float[NUM_ACIDI_GRASSI], stdGirasoleALino = new float[NUM_ACIDI_GRASSI],
            stdGirasoleAOle = new float[NUM_ACIDI_GRASSI], stdSesamoRaff = new float[NUM_ACIDI_GRASSI], stdNocciola = new float[NUM_ACIDI_GRASSI], stdOliva = new float[NUM_ACIDI_GRASSI], stdBurroCacao = new float[NUM_ACIDI_GRASSI],
            stdBabassu = new float[NUM_ACIDI_GRASSI], stdKarite = new float[NUM_ACIDI_GRASSI], stdBurro = new float[NUM_ACIDI_GRASSI], stdStrutto = new float[NUM_ACIDI_GRASSI], stdSegoRaf = new float[NUM_ACIDI_GRASSI],
			stdX = new float[NUM_ACIDI_GRASSI], stdY = new float[NUM_ACIDI_GRASSI], stdZ = new float[NUM_ACIDI_GRASSI];

        public InsermentoDati()
        {
            InitializeComponent();
            cmbStepPercent.SelectedIndex = 0;
			
			//inizializza la form di controllo della licenza
			Form FormLicenza = new GestoreLicenza();
			FormLicenza.Activate();
            LoadSTD();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float C4, C6, C8, C10, C12, C14, C14m, C15, C16ISO, C16, C16m, C17ISO, C17, C17m,
                C18, C18ISO, C18m, C18mm, C18mmm, C18CON, C20, C20m, C22, C22m;
            int StepPercent, PalmaRafMin, PalmaRafMax, PalmaAfrMin, PalmaAfrMax,
                PalmaOle60Min, PalmaOle60Max, PalmaOle62Min, PalmaOle62Max, PalmaOle64Min, PalmaOle64Max,
                PalmaSt48Min, PalmaSt48Max, PalmaSt53Min, PalmaSt53Max, CoccoRafMin, CoccoRafMax, CoccoIdrMin,
                CoccoIdrMax, PalmistoRMin, PalmistoRMax, PalmistoIMin, PalmistoIMax, PalmistoStMin,
                PalmistoStMax, PalmistoFrazMin, PalmistoFrazMax, SoiaRafMin, SoiaRafMax, ColzaRafMin, ColzaRafMax,
                ArachideRMin, ArachideRMax, VinaccioloMin, VinaccioloMax, MaisRafMin, MaisRafMax,
                GirasoleALinoMin, GirasoleALinoMax, GirasoleAOleMin, GirasoleAOleMax, SesamoRaffMin, SesamoRaffMax,
                NocciolaMin, NocciolaMax, OlivaMin, OlivaMax, BurroCacaoMin, BurroCacaoMax, BabassuMin, BabassuMax,
                KariteMin, KariteMax, BurroMin, BurroMax, StruttoMin, StruttoMax, SegoRafMin, SegoRafMax,
				XMin, XMax, YMin, YMax, ZMin, ZMax;

			//assegna variabili con valori dei campi di testo a video
            //acidi grassi
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
           
            //grassi
            int.TryParse(txtPalmaRafMin.Text,out PalmaRafMin);
            int.TryParse(txtPalmaRafMax.Text, out PalmaRafMax);
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
			int.TryParse(txtXMin.Text, out XMin);
			int.TryParse(txtYMin.Text, out YMin);
			int.TryParse(txtZMin.Text, out ZMin);
			int.TryParse(txtXMax.Text, out XMax);
			int.TryParse(txtYMax.Text, out YMax);
			int.TryParse(txtZMax.Text, out ZMax);
			
            switch (cmbStepPercent.SelectedIndex)
            {
                case 0:
                    StepPercent = 1;
                    break;
                case 1:
                    StepPercent = 2;
                    break;
                case 2:
                    StepPercent = 5;
                    break;
                case 3:
                    StepPercent = 10;
                    break;
                case 4:
                    StepPercent = 20;
                    break;
                default:
                    StepPercent = 1;
                    break;
            }

            //MessageBox.Show(txtPalmaRafMax.Text + "-" + PalmaRafMax.ToString());
            Form FormElaborazione = new Elaborazione(StepPercent,
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
                KariteMin, KariteMax, BurroMin, BurroMax, StruttoMin, StruttoMax, SegoRafMin, SegoRafMax,
			    XMin, XMax, YMin, YMax, ZMin, ZMax, 
                //standard
                stdPalmaRaf, stdPalmaAfr, stdPalmaOle60, stdPalmaOle62, stdPalmaOle64, stdPalmaSt48, stdPalmaSt53,
                stdCoccoRaf, stdCoccoIdr, stdPalmistoR, stdPalmistoI, stdPalmistoSt, stdPalmistoFraz, stdSoiaRaf,
                stdColzaRaf, stdArachideR, stdVinacciolo, stdMaisRaf, stdGirasoleALino, stdGirasoleAOle,
                stdSesamoRaff, stdNocciola, stdOliva, stdBurroCacao, stdBabassu, stdKarite, stdBurro, stdStrutto, stdSegoRaf,
			    stdX, stdY, stdZ);
            FormElaborazione.Show();
        }

        private void LoadSTD()
        {
            //carica gli standard da file!
            string FileStandard = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "default.ini";
            if (File.Exists(FileStandard))
            {
                StreamReader re = File.OpenText(FileStandard);
                string strInput = null; //valore letto dal file
                string[] TMPVal;//variabile stringa che contiene i valori degli standard in formato testo
                int tmpInd;
                while ((strInput = re.ReadLine()) != null)//cicla tutto il file
                {
                    if (strInput.Length > 0) //controlla che la stringa sia lunga almeno 1 carattere...
                    {
                        if ((strInput[0] != '#') && (strInput != "") && (strInput != " "))//esclude i commenti e le righe vuote
                        {
                            //sostituisce la virgola con il separatore decimali in uso sul sistema
                            strInput = strInput.Replace(",", System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString());
                            TMPVal = strInput.Split(' ', ';');
                            if (TMPVal.Length == NUM_ACIDI_GRASSI + 1)//controlla che ci siano tutti gli acidi grassi previsti dal programma
                            {
                                for (tmpInd = 0; tmpInd < NUM_ACIDI_GRASSI; tmpInd++)
                                {
                                    switch (TMPVal[0])//carica gli standard di ogni grasso previsto
                                    {
                                        case "PalmaRaff":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaRaf[tmpInd]);
                                            //MessageBox.Show("Indice: " + tmpInd + "\nValore: " + stdPalmaRaf[tmpInd].ToString());
                                            break;
                                        case "PalmaAfrica":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaAfr[tmpInd]);
                                            break;
                                        case "PalmaOleinIV60":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaOle60[tmpInd]);
                                            break;
                                        case "PalmaOleinIV62":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaOle62[tmpInd]);
                                            break;
                                        case "PalmaOleinIV64":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaOle64[tmpInd]);
                                            break;
                                        case "PalmaStearin48":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaSt48[tmpInd]);
                                            break;
                                        case "PalmaStearin53":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmaSt53[tmpInd]);
                                            break;
                                        case "CoccoRaff":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdCoccoRaf[tmpInd]);
                                            break;
                                        case "CoccoIdr":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdCoccoIdr[tmpInd]);
                                            break;
                                        case "PalmistoRaff":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmistoR[tmpInd]);
                                            break;
                                        case "PalmistoIdr":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmistoI[tmpInd]);
                                            break;
                                        case "PalmistoOle":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmistoSt[tmpInd]);
                                            break;
                                        case "PalmistoFraz":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdPalmistoFraz[tmpInd]);
                                            break;
                                        case "SoiaRaff":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdSoiaRaf[tmpInd]);
                                            break;
                                        case "Colza":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdColzaRaf[tmpInd]);
                                            break;
                                        case "Arachide":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdArachideR[tmpInd]);
                                            break;
                                        case "Vinacciolo":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdVinacciolo[tmpInd]);
                                            break;
                                        case "Mais":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdMaisRaf[tmpInd]);
                                            break;
                                        case "GirasoleAltoLinoleico":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdGirasoleALino[tmpInd]);
                                            break;
                                        case "GirasoleAltoOleico":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdGirasoleAOle[tmpInd]);
                                            break;
                                        case "SesamoRaff":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdSesamoRaff[tmpInd]);
                                            break;
                                        case "Nocciola":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdNocciola[tmpInd]);
                                            break;
                                        case "Oliva":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdOliva[tmpInd]);
                                            break;
                                        case "BurroDiCacao":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdBurroCacao[tmpInd]);
                                            break;
                                        case "Babassu":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdBabassu[tmpInd]);
                                            break;
                                        case "Karite":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdKarite[tmpInd]);
                                            break;
                                        case "Burro":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdBurro[tmpInd]);
                                            break;
                                        case "Strutto":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdStrutto[tmpInd]);
                                            break;
                                        case "Sego":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdSegoRaf[tmpInd]);
                                            break;
										case "X":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdX[tmpInd]);
                                            break;
										case "Y":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdY[tmpInd]);
                                            break;
										case "Z":
                                            float.TryParse(TMPVal[tmpInd + 1], out stdZ[tmpInd]);
                                            break;
                                        default:
                                            //grasso sconosciuto
                                            MessageBox.Show("Riga non valida: " + strInput);
                                            break;
                                    }
                                }
                            }
                            else//riga rotta
                            {
                                MessageBox.Show("Riga problematica: " + strInput + "\nElementi: " + TMPVal.Length + "\nPrevisti: " + (NUM_ACIDI_GRASSI + 1).ToString());
                            }
                        }
                    }
                }
                re.Close();
                re = null;
                btnElabora.Enabled = true;
            }
            else
            {
                MessageBox.Show("File degli standard non trovato:\n" + FileStandard);
                btnElabora.Enabled = false;
            }
        }

        private void esciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabellaDegliStandardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormStandard = new Standard(stdPalmaRaf, stdPalmaAfr, stdPalmaOle60, stdPalmaOle62, stdPalmaOle64, stdPalmaSt48, stdPalmaSt53,
                stdCoccoRaf, stdCoccoIdr, stdPalmistoR, stdPalmistoI, stdPalmistoSt, stdPalmistoFraz, stdSoiaRaf,
                stdColzaRaf, stdArachideR, stdVinacciolo, stdMaisRaf, stdGirasoleALino, stdGirasoleAOle,
                stdSesamoRaff, stdNocciola, stdOliva, stdBurroCacao, stdBabassu, stdKarite, stdBurro, stdStrutto, stdSegoRaf,
			    stdX, stdY, stdZ);
            FormStandard.ShowDialog();
			LoadSTD();
        }

        private void ricaricaGliStandardDaFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSTD();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormInfo = new AboutBoxMet();
            FormInfo.Show();
        }
		
		private void LicenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FormLicenza = new GestoreLicenza();
            FormLicenza.Show();
        }

        private void btnClsGrassi_Click(object sender, EventArgs e)
        {
            ClearTextControls(flowLayoutPanel2);
        }

        private void ClearTextControls(Control parent) //pulisce tutte le MaskedTextBox nel pannello passato come argomento
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is MaskedTextBox)
                {
                    (ctrl as MaskedTextBox).Text = "";
                }
 
                ClearTextControls(ctrl);
            }
        }
    }
}