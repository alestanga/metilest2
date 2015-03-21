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
using Microsoft.Win32;
using System.Globalization;

namespace metilest2009
{
    public partial class GestoreLicenza : Form
    {
				const int NUMERO_GIORNI_DEMO = 3000; //numero di giorni del periodo di prova -1=nessun periodo di prova

        //chiavi di registro
        RegistryKey CurrentUserKey = Microsoft.Win32.Registry.CurrentUser;
        RegistryKey OurAppRootKey = null;
        //RegistryKey ConfigKey = null;
        RegistryKey LicenceKey = null;
        RegistryKey InfoKey = null;
        bool ChiudiApplicazione = false;//variabile che determina se chiudere solo la form o tutta l'applicazione

        public GestoreLicenza()
        {
            InitializeComponent();

            string OurAppKeyStr = @"SOFTWARE\Metilest2"; //imposta la root del progetto nel registro
            OurAppRootKey = CurrentUserKey.CreateSubKey(OurAppKeyStr); //crea la root, se non esiste gia

            InfoKey = OurAppRootKey.CreateSubKey("Info");
			if (InfoKey.GetValue("LASTRUN") != null)//controlla se esiste il la chiave contenente la data dell ultimo utilizzo
            {
                DateTime DataUltimoUtilizzo = DateTime.Parse(CryptEngine.Decrypt(InfoKey.GetValue("LASTRUN").ToString(), true, 2));
                int VerificaDataUltimoUtilizzo = DataUltimoUtilizzo.CompareTo(DateTime.Today);
                //MessageBox.Show(VerificaDataUltimoUtilizzo.ToString() + " - " + DataUltimoUtilizzo.ToShortDateString() + " - " + DateTime.Today.ToShortDateString());
                if (VerificaDataUltimoUtilizzo > 0)
                {
                    //qualcosa non va, stanno barando sulla data -1=precedente 0=attuale 1=successiva
                    MessageBox.Show("Controlla la data del Computer!");
                   	Application.Exit();
                    //ChiudiApplicazione = true;
                    this.ShowDialog();
                }
            }
			InfoKey.SetValue("LASTRUN", CryptEngine.Encrypt(DateTime.Today.ToShortDateString(), true, 2), RegistryValueKind.String);
            InfoKey.Flush();
			
            LicenceKey = OurAppRootKey.CreateSubKey("Licence");
            if (LicenceKey.GetValue("KEY") == null)
            {//se non e' inserita la licenza va in modalita demo
                this.DemoMode();
            }
            else
            {
                //modalita licenza
                if (checkLicence(LicenceKey.GetValue("KEY").ToString(),false))
                {//controlla se la licenza e' valida e attiva
                    this.LicencedMode();
                }
                else
                {//licenza non valida
                    this.DemoMode();
                }

            }
        }

        private bool checkLicence(string vLicenceKey,bool vStoreLicence)//riceve la licenza criptata e un parametro per stabilire se memorizzare o meno la licenza, se valida
        {
            string LicenzaDec; //stringa contenente la licenza decodificata
								//formato NOME_AZIENDA;VALIDO;GG/MM/AAAA
            string[] Licenza; //array contenente la licenza
            try
            {
                LicenzaDec = CryptEngine.Decrypt(vLicenceKey, true, 1); //decrypta la licenza
                Licenza = LicenzaDec.Split(';'); //inserisce la licenza nell array
                //0=Nome azienda-1=VALIDO-2=Data validita licenza
                if (Licenza.Length == 3)
                {
					//controlla la data della licenza che e' in formato italiano GG/MM/AAAA
					DateTime DataValiditaLicenza = DateTime.ParseExact(Licenza[2], @"dd/MM/yyyy",CultureInfo.CreateSpecificCulture("it-IT"));
                    if ((Licenza[1] == "VALIDO") && (DataValiditaLicenza.CompareTo(DateTime.Today) > 0))
                    {
                        //licenza valida
                        lblModo.Text = "Concesso in licenza a " + Licenza[0] + "\nLicenza valida fino al " +
                        DataValiditaLicenza.Day.ToString() + "/" + DataValiditaLicenza.Month.ToString() + "/" + DataValiditaLicenza.Year.ToString();
                        ChiudiApplicazione = false;
						if (vStoreLicence){
							LicenceKey.SetValue("KEY", vLicenceKey, RegistryValueKind.String);
							LicenceKey.Flush();
						}
                        return true;
                    }
                    else
                    {
                        //licenza non valida
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        private void LicencedMode()
        {
            txtLicenza.Visible = false;
            btnInsLicenza.Visible = false;
        }

        private void DemoMode()
        {
            txtLicenza.Visible = true;
            btnInsLicenza.Visible = true;

            if (InfoKey.GetValue("1STRUN") == null)
            {//se non e' mai stato eseguito il programma crea la chiave contenente la data della prima esecuzione
                InfoKey.SetValue("1STRUN", CryptEngine.Encrypt(DateTime.Today.ToShortDateString(), true, 2), RegistryValueKind.String);
                InfoKey.Flush();
            }

            DateTime DataPrimoUtilizzo = DateTime.Parse(CryptEngine.Decrypt(InfoKey.GetValue("1STRUN").ToString(), true, 2));

            //controlla se e' scaduto il periodo di prova
			int VerificaDataDemo = DateTime.Today.Subtract(DataPrimoUtilizzo).Days;
			//MessageBox.Show(VerificaDataDemo.ToString() +" > " + ( NUMERO_GIORNI_DEMO).ToString());
            if (Math.Abs(VerificaDataDemo) > NUMERO_GIORNI_DEMO)
            {
                //MessageBox.Show("Periodo di prova terminato");
				MessageBox.Show("Nessuna licenza");
                //Application.Exit();
                ChiudiApplicazione = true;
				lblModo.Text="Inserisci un codice di licenza valido per continuare";
                this.ShowDialog();

            }
			else
			{
				lblModo.Text = "Demo - Installato il giorno: " + DataPrimoUtilizzo.Day.ToString() + "/" + DataPrimoUtilizzo.Month.ToString() + "/" + DataPrimoUtilizzo.Year.ToString() +
                "\nRestano ancora " + (NUMERO_GIORNI_DEMO + VerificaDataDemo).ToString() + " giorni di prova";
			}
        }

        private void btnInsLicenza_Click_1(object sender, EventArgs e)
        {
            if (txtLicenza.Text.Length > 0){
	            if (checkLicence(txtLicenza.Text,true))//controlla se la licenza e' valida e attiva
	            {
	                this.LicencedMode();
	            }
	            else
	            {
	                MessageBox.Show("La licenza inserita non e' valida!");
	                this.DemoMode();
	            }
			}
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        //evento generato dalla chiusura della form
        private void GestoreLicenza_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ChiudiApplicazione)//ad esempio licenza scaduta
            {
                Application.Exit();
            }
        }

        private void GestoreLicenza_Load(object sender, EventArgs e)
        {

        }
    }
}