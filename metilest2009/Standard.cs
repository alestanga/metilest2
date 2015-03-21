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
    public partial class Standard : Form
    {
        //costanti
        public const int NUM_ACIDI_GRASSI = 24; //totale acidi grassi analizzati
        public const int NUM_GRASSI = 32;
		
        public Standard(//standard
            float[] vstdPalmaRaf, float[] vstdPalmaAfr, float[] vstdPalmaOle60, float[] vstdPalmaOle62, float[] vstdPalmaOle64,
            float[] vstdPalmaSt48, float[] vstdPalmaSt53, float[] vstdCoccoRaf, float[] vstdCoccoIdr, float[] vstdPalmistoR,
            float[] vstdPalmistoI, float[] vstdPalmistoSt, float[] vstdPalmistoFraz, float[] vstdSoiaRaf, float[] vstdColzaRaf,
            float[] vstdArachideR, float[] vstdVinacciolo, float[] vstdMaisRaf, float[] vstdGirasoleALino, float[] vstdGirasoleAOle,
            float[] vstdSesamoRaff, float[] vstdNocciola, float[] vstdOliva, float[] vstdBurroCacao, float[] vstdBabassu, float[] vstdKarite,
            float[] vstdBurro, float[] vstdStrutto, float[] vstdSegoRaf, float[] vstdX, float[] vstdY, float[] vstdZ)
        {
            float[][] ArrayStandard = new float[NUM_GRASSI][];
            int IndRiga, IndColonna;
            string HeaderColonna, HeaderRiga = null;

            ArrayStandard[0] = vstdPalmaRaf;
            ArrayStandard[1] = vstdPalmaAfr;
            ArrayStandard[2] = vstdPalmaOle60;
            ArrayStandard[3] = vstdPalmaOle62;
            ArrayStandard[4] = vstdPalmaOle64;
            ArrayStandard[5] = vstdPalmaSt48;
            ArrayStandard[6] = vstdPalmaSt53;
            ArrayStandard[7] = vstdCoccoRaf;
            ArrayStandard[8] = vstdCoccoIdr;
            ArrayStandard[9] = vstdPalmistoR;
            ArrayStandard[10] = vstdPalmistoI;
            ArrayStandard[11] = vstdPalmistoSt;
            ArrayStandard[12] = vstdPalmistoFraz;
            ArrayStandard[13] = vstdSoiaRaf;
            ArrayStandard[14] = vstdColzaRaf;
            ArrayStandard[15] = vstdArachideR;
            ArrayStandard[16] = vstdVinacciolo;
            ArrayStandard[17] = vstdMaisRaf;
            ArrayStandard[18] = vstdGirasoleALino;
            ArrayStandard[19] = vstdGirasoleAOle;
            ArrayStandard[20] = vstdSesamoRaff;
            ArrayStandard[21] = vstdNocciola;
            ArrayStandard[22] = vstdOliva;
            ArrayStandard[23] = vstdBurroCacao;
            ArrayStandard[24] = vstdBabassu;
            ArrayStandard[25] = vstdKarite;
            ArrayStandard[26] = vstdBurro;
            ArrayStandard[27] = vstdStrutto;
            ArrayStandard[28] = vstdSegoRaf;
			ArrayStandard[29] = vstdX;
			ArrayStandard[30] = vstdY;
			ArrayStandard[31] = vstdZ;
            InitializeComponent();

            dataGridSTD.ReadOnly = false; //permette o inibisce scrittura della tabella
            dataGridSTD.RowCount = NUM_GRASSI;
            dataGridSTD.ColumnCount = NUM_ACIDI_GRASSI;
            
            //imposta header delle colonne
            for (IndColonna = 0; IndColonna < NUM_ACIDI_GRASSI; IndColonna++)
            {
                switch (IndColonna)
                {
                    case 0:
                        HeaderColonna = "C4:0";
                        break;
                    case 1:
                        HeaderColonna = "C6:0";
                        break;
                    case 2:
                        HeaderColonna = "C8:0";
                        break;
                    case 3:
                        HeaderColonna = "C10:0";
                        break;
                    case 4:
                        HeaderColonna = "C12:0";
                        break;
                    case 5:
                        HeaderColonna = "C14:0";
                        break;
                    case 6:
                        HeaderColonna = "C14:1";
                        break;
                    case 7:
                        HeaderColonna = "C15:0";
                        break;
                    case 8:
                        HeaderColonna = "C16:ISO";
                        break;
                    case 9:
                        HeaderColonna = "C16:0";
                        break;
                    case 10:
                        HeaderColonna = "C16:1";
                        break;
                    case 11:
                        HeaderColonna = "C17:ISO";
                        break;
                    case 12:
                        HeaderColonna = "C17:0";
                        break;
                    case 13:
                        HeaderColonna = "C17:1";
                        break;
                    case 14:
                        HeaderColonna = "C18:0";
                        break;
                    case 15:
                        HeaderColonna = "C18:ISO";
                        break;
                    case 16:
                        HeaderColonna = "C18:1";
                        break;
                    case 17:
                        HeaderColonna = "C18:2";
                        break;
                    case 18:
                        HeaderColonna = "C18:3";
                        break;
                    case 19:
                        HeaderColonna = "C18:CON";
                        break;
                    case 20:
                        HeaderColonna = "C20:0";
                        break;
                    case 21:
                        HeaderColonna = "C20:1";
                        break;
                    case 22:
                        HeaderColonna = "C22:0";
                        break;
                    case 23:
                        HeaderColonna = "C22:1";
                        break;
                    default:
                        HeaderColonna = "Errore";
                        break;
                }
                dataGridSTD.Columns[IndColonna].HeaderText = HeaderColonna;
            }
            
            for (IndRiga = 0; IndRiga < NUM_GRASSI; IndRiga++)
            {
                switch (IndRiga)//imposta indici riga
                {
                    case 0:
                        HeaderRiga = "Palma";
                        break;
                    case 1:
                        HeaderRiga = "Palma africano";
                        break;
                    case 2:
                        HeaderRiga = "Palma olein. IV 60";
                        break;
                    case 3:
                        HeaderRiga = "Palma olein. IV 62";
                        break;
                    case 4:
                        HeaderRiga = "Palma olein. IV 64";
                        break;
                    case 5:
                        HeaderRiga = "Palma stearin. 48";
                        break;
                    case 6:
                        HeaderRiga = "Palma stearin. 53";
                        break;
                    case 7:
                        HeaderRiga = "Cocco";
                        break;
                    case 8:
                        HeaderRiga = "Cocco idrogenato";
                        break;
                    case 9:
                        HeaderRiga = "Palmisti";
                        break;
                    case 10:
                        HeaderRiga = "Palmisti idr.";
                        break;
                    case 11:
                        HeaderRiga = "Palmisti olei.";
                        break;
                    case 12:
                        HeaderRiga = "Palmisti fraz.";
                        break;
                    case 13:
                        HeaderRiga = "Soia";
                        break;
                    case 14:
                        HeaderRiga = "Colza";
                        break;
                    case 15:
                        HeaderRiga = "Arachide";
                        break;
                    case 16:
                        HeaderRiga = "Vinacciolo";
                        break;
                    case 17:
                        HeaderRiga = "Mais";
                        break;
                    case 18:
                        HeaderRiga = "Girasole alto linoleico";
                        break;
                    case 19:
                        HeaderRiga = "Girasole alto oleico";
                        break;
                    case 20:
                        HeaderRiga = "Sesamo";
                        break;
                    case 21:
                        HeaderRiga = "Nocciola";
                        break;
                    case 22:
                        HeaderRiga = "Oliva";
                        break;
                    case 23:
                        HeaderRiga = "Burro cacao";
                        break;
                    case 24:
                        HeaderRiga = "Babassu";
                        break;
                    case 25:
                        HeaderRiga = "Karitè";
                        break;
                    case 26:
                        HeaderRiga = "Burro";
                        break;
                    case 27:
                        HeaderRiga = "Strutto";
                        break;
                    case 28:
                        HeaderRiga = "Sego";
                        break;
					case 29:
                        HeaderRiga = "X";
                        break;
					case 30:
                        HeaderRiga = "Y";
                        break;
					case 31:
                        HeaderRiga = "Z";
                        break;
                    default:
                        HeaderRiga = "Errore";
                        break;
                }

                dataGridSTD.Rows[IndRiga].HeaderCell.Value = HeaderRiga;
                for (IndColonna = 0; IndColonna < NUM_ACIDI_GRASSI; IndColonna++)
                {
                    dataGridSTD[IndColonna, IndRiga].Value = ArrayStandard[IndRiga][IndColonna];
                }
            }
        }

		private void WriteSTD()
        {
            //carica gli standard da file!
            string FileStandard = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "default.ini";
            StreamWriter StrWr = new StreamWriter(FileStandard,false);
            string strOutput = null; //valore letto dal file
            int IndRiga, IndColonna;
            for (IndRiga = 0; IndRiga < NUM_GRASSI; IndRiga++)
            {
                switch (IndRiga)//imposta indici riga
                {
                    case 0:
                        strOutput = "PalmaRaff";
                        break;
                    case 1:
                        strOutput = "PalmaAfrica";
                        break;
                    case 2:
                        strOutput = "PalmaOleinIV60";
                        break;
                    case 3:
                        strOutput = "PalmaOleinIV62";
                        break;
                    case 4:
                        strOutput = "PalmaOleinIV64";
                        break;
                    case 5:
                        strOutput = "PalmaStearin48";
                        break;
                    case 6:
                        strOutput = "PalmaStearin53";
                        break;
                    case 7:
                        strOutput = "CoccoRaff";
                        break;
                    case 8:
                        strOutput = "CoccoIdr";
                        break;
                    case 9:
                        strOutput = "PalmistoRaff";
                        break;
                    case 10:
                        strOutput = "PalmistoIdr";
                        break;
                    case 11:
                        strOutput = "PalmistoOle";
                        break;
                    case 12:
                        strOutput = "PalmistoFraz";
                        break;
                    case 13:
                        strOutput = "SoiaRaff";
                        break;
                    case 14:
                        strOutput = "Colza";
                        break;
                    case 15:
                        strOutput = "Arachide";
                        break;
                    case 16:
                        strOutput = "Vinacciolo";
                        break;
                    case 17:
                        strOutput = "Mais";
                        break;
                    case 18:
                        strOutput = "GirasoleAltoLinoleico";
                        break;
                    case 19:
                        strOutput = "GirasoleAltoOleico";
                        break;
                    case 20:
                        strOutput = "SesamoRaff";
                        break;
                    case 21:
                        strOutput = "Nocciola";
                        break;
                    case 22:
                        strOutput = "Oliva";
                        break;
                    case 23:
                        strOutput = "BurroDiCacao";
                        break;
                    case 24:
                        strOutput = "Babassu";
                        break;
                    case 25:
                        strOutput = "Karite";
                        break;
                    case 26:
                        strOutput = "Burro";
                        break;
                    case 27:
                        strOutput = "Strutto";
                        break;
                    case 28:
                        strOutput = "Sego";
                        break;
                    case 29:
                        strOutput = "X";
                        break;
                    case 30:
                        strOutput = "Y";
                        break;
                    case 31:
                        strOutput = "Z";
                        break;              
					default:
						strOutput = "Errore";
						break;
					}
                //aggiunge il punto e virgola dopo il nome del grasso
				strOutput += ";";
                    for (IndColonna = 0; IndColonna < NUM_ACIDI_GRASSI; IndColonna++)
                    {
						//sostituisce la virgola con il separatore decimali in uso sul sistema
						//per rendere compatibile il file con tutti i locali
					    strOutput += dataGridSTD[IndColonna, IndRiga].Value.ToString().Replace(System.Globalization.CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator.ToString(), ",");
						if ( IndColonna < NUM_ACIDI_GRASSI - 1)
						{
							strOutput += ";";
						}
                } 
				StrWr.WriteLine(strOutput);
        		}
			StrWr.Close();
            StrWr = null;
		}

		private void salvaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteSTD();
        }
		
        private void chiudiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}