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
using System.Reflection;
using unoidl.com.sun.star.lang;
using unoidl.com.sun.star.uno;
using unoidl.com.sun.star.bridge;
using unoidl.com.sun.star.frame;
using unoidl.com.sun.star.text;
using unoidl.com.sun.star.beans;
using System.Threading;
using System.IO;

namespace metilest2009
{
    public partial class Report : Form
    {
        BackgroundWorker thOpenOffice;
        private int oPalmaRaf, oPalmaAfr, oPalmaOle60, oPalmaOle62, oPalmaOle64, oPalmaSt48, oPalmaSt53,
            oCoccoRaf, oCoccoIdr, oPalmistoR, oPalmistoI, oPalmistoSt, oPalmistoFraz, oSoiaRaf,
            oColzaRaf, oArachideR, oVinacciolo, oMaisRaf, oGirasoleALino, oGirasoleAOle, oSesamoRaff,
            oNocciola, oOliva, oBurroCacao, oBabassu, oKarite, oBurro, oStrutto, oSegoRaf, oX, oY, oZ;
        private float[] AcidiGrassiOtt, AcidiGrassiRic;
        private float oNiodio;
        private string SaveTo,Estensione;

        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        public Report(int vPalmaRaf, int vPalmaAfr, int vPalmaOle60, int vPalmaOle62, int vPalmaOle64, int vPalmaSt48, int vPalmaSt53,
            int vCoccoRaf, int vCoccoIdr, int vPalmistoR, int vPalmistoI, int vPalmistoSt, int vPalmistoFraz, int vSoiaRaf,
            int vColzaRaf, int vArachideR, int vVinacciolo, int vMaisRaf, int vGirasoleALino, int vGirasoleAOle, int vSesamoRaff,
            int vNocciola, int vOliva, int vBurroCacao, int vBabassu, int vKarite, int vBurro, int vStrutto, int vSegoRaf,
		    int vX, int vY, int vZ,
            float[] vAcidiGrassiOtt, float vNIodio, float vIndApprOtt, float[] vAcidiGrassiRic)
        {
            InitializeComponent();

			//assegna label verdi
            SetControlPropertyThreadSafe(lblPalmaRaffV, "Text", vPalmaRaf.ToString());
            SetControlPropertyThreadSafe(lblPalmaAfrV, "Text", vPalmaAfr.ToString());
            SetControlPropertyThreadSafe(lblPalmaOle60V, "Text", vPalmaOle60.ToString());
            SetControlPropertyThreadSafe(lblPalmaOle62V, "Text", vPalmaOle62.ToString());
            SetControlPropertyThreadSafe(lblPalmaOle64V, "Text", vPalmaOle64.ToString());
            SetControlPropertyThreadSafe(lblPalmaSt48V, "Text", vPalmaSt48.ToString());
            SetControlPropertyThreadSafe(lblPalmaSt53V, "Text", vPalmaSt53.ToString());
            SetControlPropertyThreadSafe(lblCoccoRaffV, "Text", vCoccoRaf.ToString());
            SetControlPropertyThreadSafe(lblCoccoidrV, "Text", vCoccoIdr.ToString());
            SetControlPropertyThreadSafe(lblPalmistoRV, "Text", vPalmistoR.ToString());
            SetControlPropertyThreadSafe(lblPalmistoIV, "Text", vPalmistoI.ToString());
            SetControlPropertyThreadSafe(lblPalmistoSTV, "Text", vPalmistoSt.ToString());
            SetControlPropertyThreadSafe(lblPalmistoFrazV, "Text", vPalmistoFraz.ToString());
            SetControlPropertyThreadSafe(lblSoiaRaffV, "Text", vSoiaRaf.ToString());
            SetControlPropertyThreadSafe(lblColzaRaffV, "Text", vColzaRaf.ToString());
            SetControlPropertyThreadSafe(lblArachideRV, "Text", vArachideR.ToString());
            SetControlPropertyThreadSafe(lblVinaccioloV, "Text", vVinacciolo.ToString());
            SetControlPropertyThreadSafe(lblMaisRaffV, "Text", vMaisRaf.ToString());
            SetControlPropertyThreadSafe(lblGirasoleALinoV, "Text", vGirasoleALino.ToString());
            SetControlPropertyThreadSafe(lblGirasoleAOleV, "Text", vGirasoleAOle.ToString());
            SetControlPropertyThreadSafe(lblSesamoRaffV, "Text", vSesamoRaff.ToString());
            SetControlPropertyThreadSafe(lblNocciolaV, "Text", vNocciola.ToString());
            SetControlPropertyThreadSafe(lblOlivaV, "Text", vOliva.ToString());
            SetControlPropertyThreadSafe(lblBurroCacaoV, "Text", vBurroCacao.ToString());
            SetControlPropertyThreadSafe(lblBabassuV, "Text", vBabassu.ToString());
            SetControlPropertyThreadSafe(lblKariteV, "Text", vKarite.ToString());
            SetControlPropertyThreadSafe(lblBrurroV, "Text", vBurro.ToString());
            SetControlPropertyThreadSafe(lblStruttoV, "Text", vStrutto.ToString());
            SetControlPropertyThreadSafe(lblSegoRaffV, "Text", vSegoRaf.ToString());
			SetControlPropertyThreadSafe(lblXV, "Text", vX.ToString());
			SetControlPropertyThreadSafe(lblYV, "Text", vY.ToString());
			SetControlPropertyThreadSafe(lblZV, "Text", vZ.ToString());
			
            SetControlPropertyThreadSafe(lblNIodio, "Text", vNIodio.ToString("F1"));
            SetControlPropertyThreadSafe(lblIndApprOttV, "Text", vIndApprOtt.ToString());

            SetControlPropertyThreadSafe(lblC4V, "Text", vAcidiGrassiOtt[0].ToString());
            SetControlPropertyThreadSafe(lblC6V, "Text", vAcidiGrassiOtt[1].ToString());
            SetControlPropertyThreadSafe(lblC8V, "Text", vAcidiGrassiOtt[2].ToString());
            SetControlPropertyThreadSafe(lblC10V, "Text", vAcidiGrassiOtt[3].ToString());
            SetControlPropertyThreadSafe(lblC12V, "Text", vAcidiGrassiOtt[4].ToString());
            SetControlPropertyThreadSafe(lblC14V, "Text", vAcidiGrassiOtt[5].ToString());
            SetControlPropertyThreadSafe(lblC14mV, "Text", vAcidiGrassiOtt[6].ToString());
            SetControlPropertyThreadSafe(lblC15V, "Text", vAcidiGrassiOtt[7].ToString());
            SetControlPropertyThreadSafe(lblC16ISOV, "Text", vAcidiGrassiOtt[8].ToString());
            SetControlPropertyThreadSafe(lblC16V, "Text", vAcidiGrassiOtt[9].ToString());
            SetControlPropertyThreadSafe(lblC16mV, "Text", vAcidiGrassiOtt[10].ToString());
            SetControlPropertyThreadSafe(lblC17ISOV, "Text", vAcidiGrassiOtt[11].ToString());
            SetControlPropertyThreadSafe(lblC17V, "Text", vAcidiGrassiOtt[12].ToString());
            SetControlPropertyThreadSafe(lblC17mV, "Text", vAcidiGrassiOtt[13].ToString());
            SetControlPropertyThreadSafe(lblC18V, "Text", vAcidiGrassiOtt[14].ToString());
            SetControlPropertyThreadSafe(lblC18ISOV, "Text", vAcidiGrassiOtt[15].ToString());
            SetControlPropertyThreadSafe(lblC18mV, "Text", vAcidiGrassiOtt[16].ToString());
            SetControlPropertyThreadSafe(lblC18mmV, "Text", vAcidiGrassiOtt[17].ToString());
            SetControlPropertyThreadSafe(lblC18mmmV, "Text", vAcidiGrassiOtt[18].ToString());
            SetControlPropertyThreadSafe(lblC18CONV, "Text", vAcidiGrassiOtt[19].ToString());
            SetControlPropertyThreadSafe(lblC20V, "Text", vAcidiGrassiOtt[20].ToString());
            SetControlPropertyThreadSafe(lblC20mV, "Text", vAcidiGrassiOtt[21].ToString());
            SetControlPropertyThreadSafe(lblC22V, "Text", vAcidiGrassiOtt[22].ToString());
            SetControlPropertyThreadSafe(lblC22mV, "Text", vAcidiGrassiOtt[23].ToString());

            oPalmaRaf = vPalmaRaf;
            oPalmaAfr = vPalmaAfr;
            oPalmaOle60 = vPalmaOle60;
            oPalmaOle62 = vPalmaOle62;
            oPalmaOle64 = vPalmaOle64;
            oPalmaSt48 = vPalmaSt48;
            oPalmaSt53 = vPalmaSt53;
            oCoccoRaf = vCoccoRaf;
            oCoccoIdr = vCoccoIdr;
            oPalmistoR = vPalmistoR;
            oPalmistoI = vPalmistoI;
            oPalmistoSt = vPalmistoSt;
            oPalmistoFraz = vPalmistoFraz;
            oSoiaRaf = vSoiaRaf;
            oColzaRaf = vColzaRaf;
            oArachideR = vArachideR;
            oVinacciolo = vVinacciolo;
            oMaisRaf = vMaisRaf;
            oGirasoleALino = vGirasoleALino;
            oGirasoleAOle = vGirasoleAOle;
            oSesamoRaff = vSesamoRaff;
            oNocciola = vNocciola;
            oOliva = vOliva;
            oBurroCacao = vBurroCacao;
            oBabassu = vBabassu;
            oKarite = vKarite;
            oBurro = vBurro;
            oStrutto = vStrutto;
            oSegoRaf = vSegoRaf;
			oX = vX;
			oY = vY;
			oZ = vZ;

            AcidiGrassiOtt = vAcidiGrassiOtt;
            AcidiGrassiRic = vAcidiGrassiRic;
            oNiodio = vNIodio;
        }

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
            }
        }

        private void CreaReport(object sender, DoWorkEventArgs e)
        {
            string PathTemplate = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "template.odt";
            if (File.Exists(PathTemplate))
            {
                //The C# application starts OpenOffice.org by using it's bootstrap method:
                XComponentContext oStrap = uno.util.Bootstrap.bootstrap();
                //This will start a new instance of OpenOffice.org if it is not running,
                //or it will obtain an existing instance if it is already open.
                //The next step is to create a new OpenOffice.org service manager:
                XMultiServiceFactory oServMan = (XMultiServiceFactory)oStrap.getServiceManager();

                XComponentLoader oDesk = (XComponentLoader)oServMan.createInstance("com.sun.star.frame.Desktop");

                //string url = @"private:factory/swriter";
                PropertyValue[] propVals = new PropertyValue[0];

                XComponent oDoc = oDesk.loadComponentFromURL(PathConverter(PathTemplate), "_blank", 0, propVals);

                //string docText = "This will be my first paragraph.\n\r";
                //docText += "This will be my second paragraph.\n\r";

                //And then this is written to the body of the document:
                //((XTextDocument)oDoc).getText().setString(docText);

                XBookmarksSupplier xBS = (XBookmarksSupplier)oDoc;

                unoidl.com.sun.star.container.XNameAccess xNamedBookmarks = xBS.getBookmarks();

                //inserimento dati nei bookmark
                //grassi
                InsertTxtBookmark(xNamedBookmarks, "bPalma", oPalmaRaf.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaAfr", oPalmaAfr.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaOle60", oPalmaOle60.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaOle62", oPalmaOle62.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaOle64", oPalmaOle64.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaSte48", oPalmaSt48.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmaSte53", oPalmaSt53.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bCocco", oCoccoRaf.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bCoccoIdr", oCoccoIdr.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmisto", oPalmistoR.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmistoIdr", oPalmistoI.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmistoSte", oPalmistoSt.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bPalmistoFra", oPalmistoFraz.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bSoia", oSoiaRaf.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bColza", oColzaRaf.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bArachide", oArachideR.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bVinacciolo", oVinacciolo.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bMais", oMaisRaf.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bGirasoleAO", oGirasoleALino.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bGirasoleAL", oGirasoleAOle.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bSesamo", oSesamoRaff.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bNocciola", oNocciola.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bOliva", oOliva.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bBurroCacao", oBurroCacao.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bBabassu", oBabassu.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bKarite", oKarite.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bBurro", oBurro.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bStrutto", oStrutto.ToString());
                InsertTxtBookmark(xNamedBookmarks, "bSego", oSegoRaf.ToString());
				InsertTxtBookmark(xNamedBookmarks, "bX", oX.ToString());
				InsertTxtBookmark(xNamedBookmarks, "bY", oY.ToString());
				InsertTxtBookmark(xNamedBookmarks, "bZ", oZ.ToString());
                //acidi grassi calcolati
                InsertTxtBookmark(xNamedBookmarks, "bC4-0", AcidiGrassiOtt[0].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC6-0", AcidiGrassiOtt[1].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC8-0", AcidiGrassiOtt[2].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC10-0", AcidiGrassiOtt[3].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC12-0", AcidiGrassiOtt[4].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC14-0", AcidiGrassiOtt[5].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC14-1", AcidiGrassiOtt[6].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC15-0", AcidiGrassiOtt[7].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC16-ISO", AcidiGrassiOtt[8].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC16-0", AcidiGrassiOtt[9].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC16-1", AcidiGrassiOtt[10].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC17-ISO", AcidiGrassiOtt[11].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC17-0", AcidiGrassiOtt[12].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC17-1", AcidiGrassiOtt[13].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-0", AcidiGrassiOtt[14].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-ISO", AcidiGrassiOtt[15].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-1", AcidiGrassiOtt[16].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-2", AcidiGrassiOtt[17].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-3", AcidiGrassiOtt[18].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC18-CON", AcidiGrassiOtt[19].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC20-0", AcidiGrassiOtt[20].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC20-1", AcidiGrassiOtt[21].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC22-0", AcidiGrassiOtt[22].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bC22-1", AcidiGrassiOtt[23].ToString("F2"));
                //acidi grassi inseriti a mano (forniti dal gascromatografo)
                InsertTxtBookmark(xNamedBookmarks, "bGC4-0", AcidiGrassiRic[0].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC6-0", AcidiGrassiRic[1].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC8-0", AcidiGrassiRic[2].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC10-0", AcidiGrassiRic[3].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC12-0", AcidiGrassiRic[4].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC14-0", AcidiGrassiRic[5].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC14-1", AcidiGrassiRic[6].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC15-0", AcidiGrassiRic[7].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC16-ISO", AcidiGrassiRic[8].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC16-0", AcidiGrassiRic[9].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC16-1", AcidiGrassiRic[10].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC17-ISO", AcidiGrassiRic[11].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC17-0", AcidiGrassiRic[12].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC17-1", AcidiGrassiRic[13].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-0", AcidiGrassiRic[14].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-ISO", AcidiGrassiRic[15].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-1", AcidiGrassiRic[16].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-2", AcidiGrassiRic[17].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-3", AcidiGrassiRic[18].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC18-CON", AcidiGrassiRic[19].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC20-0", AcidiGrassiRic[20].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC20-1", AcidiGrassiRic[21].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC22-0", AcidiGrassiRic[22].ToString("F2"));
                InsertTxtBookmark(xNamedBookmarks, "bGC22-1", AcidiGrassiRic[23].ToString("F2"));
                //numero di iodio
                InsertTxtBookmark(xNamedBookmarks, "bnIodio", oNiodio.ToString("F2"));

                //And then the file is saved to disk:
                //((XStorable)oDoc).storeAsURL(PathConverter(@"C:\test.odt"), propVals);
                //((XStorable)oDoc).storeAsURL(PathConverter(SaveTo), propVals);
                PropertyValue[] propValsOut = new PropertyValue[1];
                propValsOut[0] = new PropertyValue();
                propValsOut[0].Name = "FilterName";
                switch (Estensione)
                {
                    case ".odt":
                        break;
                    case ".doc":
                        propValsOut[0].Value = new uno.Any("MS Word 97");
                        break;
                    case ".pdf":
                        propValsOut[0].Value = new uno.Any("writer_pdf_Export");
                        break;
                    default:
                        break;
                }

                ((XStorable)oDoc).storeToURL(PathConverter(SaveTo), propValsOut);
                //chiude
                ((XComponent)oDoc).dispose();
                //And then any memory that's been used can be freed up:
                oDoc = null;
            }
            else
            {
                MessageBox.Show("Template non trovato:\n" + PathTemplate);
            }
        }

        //inserisce il testo nel bookmark, se va a buon fine ritorna true
        private bool InsertTxtBookmark(unoidl.com.sun.star.container.XNameAccess ElencoBookmark, string NomeBookmark, string Testo)
        {
            if (ElencoBookmark.hasByName(NomeBookmark))//verifica che esista il bookmark
            {
                XTextContent xFoundBookmark = (XTextContent)ElencoBookmark.getByName(NomeBookmark).Value;
                // work with bookmark
                XTextRange xFound = xFoundBookmark.getAnchor();
                xFound.setString(Testo);
                return true;
            }
            else
            {
                return false;
            }  
        }

        private static string PathConverter(string file)
        {
            try
            {
                file = file.Replace(@"\", "/");

                return "file:///" + file;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        void ThreadFinished(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Report_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.InitialDirectory = geoData.GetFilePath();
            saveFileDialog1.Filter = "Documento OpenOffice (*.ODT)|*.odt|Documento MS Word (*.DOC)|*.doc|Documento PDF (*.PDF)|*.pdf";
            //saveFileDialog1.Filter = "Documento OpenOffice (*.ODT)|*.odt";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            //read and filter the raw data 
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SaveTo = saveFileDialog1.FileName;
                System.IO.FileInfo fs = new System.IO.FileInfo(SaveTo);
                Estensione = fs.Extension;
                //geoData.SaveToN31File(saveFileDialog1.FileName);
                //proForm.Text = saveFileDialog1.InitialDirectory;
                thOpenOffice = new BackgroundWorker();
                thOpenOffice.DoWork += new DoWorkEventHandler(CreaReport);
                thOpenOffice.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ThreadFinished);
                thOpenOffice.WorkerSupportsCancellation = true;
                thOpenOffice.RunWorkerAsync();
            } 

        }
    }
}