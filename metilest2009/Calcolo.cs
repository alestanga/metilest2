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
using System.Threading;
using System.Reflection;

namespace metilest2009
{
    public partial class Elaborazione : Form
    {
        //costanti
        private const int NUM_ACIDI_GRASSI = 24; //totale acidi grassi analizzati
        private const float IODIO_C16M = 99.78f; //costanti per il calcolo dello iodio
        private const float IODIO_C17M = 94.9f;
        private const float IODIO_C18ISO = 89.9f;
        private const float IODIO_C18M = 89.9f;
        private const float IODIO_C18MM = 181.04f;
        private const float IODIO_C18MMM = 273.52f;
        private const float IODIO_C20M = 81.75f;
        private const float IODIO_C22M = 74.98f;

        //thread
        //Thread bwMulo;
        //Thread threadUpdateGUI;
        BackgroundWorker bwMulo;
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);

        //standard
        private float[] stdPalmaRaf = new float[NUM_ACIDI_GRASSI], stdPalmaAfr = new float[NUM_ACIDI_GRASSI], stdPalmaOle60 = new float[NUM_ACIDI_GRASSI], stdPalmaOle62 = new float[NUM_ACIDI_GRASSI],
            stdPalmaOle64 = new float[NUM_ACIDI_GRASSI], stdPalmaSt48 = new float[NUM_ACIDI_GRASSI], stdPalmaSt53 = new float[NUM_ACIDI_GRASSI], stdCoccoRaf = new float[NUM_ACIDI_GRASSI], stdCoccoIdr = new float[NUM_ACIDI_GRASSI],
            stdPalmistoR = new float[NUM_ACIDI_GRASSI], stdPalmistoI = new float[NUM_ACIDI_GRASSI], stdPalmistoSt = new float[NUM_ACIDI_GRASSI], stdPalmistoFraz = new float[NUM_ACIDI_GRASSI], stdSoiaRaf = new float[NUM_ACIDI_GRASSI],
            stdColzaRaf = new float[NUM_ACIDI_GRASSI], stdArachideR = new float[NUM_ACIDI_GRASSI], stdVinacciolo = new float[NUM_ACIDI_GRASSI], stdMaisRaf = new float[NUM_ACIDI_GRASSI], stdGirasoleALino = new float[NUM_ACIDI_GRASSI],
            stdGirasoleAOle = new float[NUM_ACIDI_GRASSI], stdSesamoRaff = new float[NUM_ACIDI_GRASSI], stdNocciola = new float[NUM_ACIDI_GRASSI], stdOliva = new float[NUM_ACIDI_GRASSI], stdBurroCacao = new float[NUM_ACIDI_GRASSI],
            stdBabassu = new float[NUM_ACIDI_GRASSI], stdKarite = new float[NUM_ACIDI_GRASSI], stdBurro = new float[NUM_ACIDI_GRASSI], stdStrutto = new float[NUM_ACIDI_GRASSI], stdSegoRaf = new float[NUM_ACIDI_GRASSI],
			stdX = new float[NUM_ACIDI_GRASSI], stdY = new float[NUM_ACIDI_GRASSI], stdZ = new float[NUM_ACIDI_GRASSI];

        //variabili
        private DateTime startTime;
        private DateTime stopTime;
        private TimeSpan duration;
        private int StepPercent, NumCfr, GrassDaElab, cntRefresh;
        private decimal NumCicli, TentativiMax;
        private float IndApprRel, IndApprOtt, //calcolo dello iodio
            oC16mIODIO, oC17mIODIO, oC18ISOIODIO, oC18mIODIO, oC18mmIODIO, oC18mmmIODIO, oC20mIODIO, oC22mIODIO, oNIodio;
        private int
            //indici per calcoli
            iPalmaRaf, iPalmaAfr, iPalmaOle60, iPalmaOle62, iPalmaOle64, iPalmaSt48, iPalmaSt53,
            iCoccoRaf, iCoccoIdr, iPalmistoR, iPalmistoI, iPalmistoSt, iPalmistoFraz, iSoiaRaf,
            iColzaRaf, iArachideR, iVinacciolo, iMaisRaf, iGirasoleALino, iGirasoleAOle, iSesamoRaff,
            iNocciola, iOliva, iBurroCacao, iBabassu, iKarite, iBurro, iStrutto, iSegoRaf, iX, iY, iZ,
            //ottimali
            oPalmaRaf, oPalmaAfr, oPalmaOle60, oPalmaOle62, oPalmaOle64, oPalmaSt48, oPalmaSt53,
            oCoccoRaf, oCoccoIdr, oPalmistoR, oPalmistoI, oPalmistoSt, oPalmistoFraz, oSoiaRaf,
            oColzaRaf, oArachideR, oVinacciolo, oMaisRaf, oGirasoleALino, oGirasoleAOle, oSesamoRaff,
            oNocciola, oOliva, oBurroCacao, oBabassu, oKarite, oBurro, oStrutto, oSegoRaf, oX, oY, oZ;

        private float[] AcidiGrassiRic = new float[NUM_ACIDI_GRASSI];//array che conterrà gli acidi grassi ricevuti
        private float[] AcidiGrassiOtt = new float[NUM_ACIDI_GRASSI];//array che conterrà gli acidi grassi ottimali
		//range di ricerca dei vari grassi
        private int PalmaRafMin, PalmaRafMax, PalmaAfrMin, PalmaAfrMax,
                PalmaOle60Min, PalmaOle60Max, PalmaOle62Min, PalmaOle62Max, PalmaOle64Min, PalmaOle64Max,
                PalmaSt48Min, PalmaSt48Max, PalmaSt53Min, PalmaSt53Max, CoccoRafMin, CoccoRafMax, CoccoIdrMin,
                CoccoIdrMax, PalmistoRMin, PalmistoRMax, PalmistoIMin, PalmistoIMax, PalmistoStMin,
                PalmistoStMax, PalmistoFrazMin, PalmistoFrazMax, SoiaRafMin, SoiaRafMax, ColzaRafMin, ColzaRafMax,
                ArachideRMin, ArachideRMax, VinaccioloMin, VinaccioloMax, MaisRafMin, MaisRafMax,
                GirasoleALinoMin, GirasoleALinoMax, GirasoleAOleMin, GirasoleAOleMax, SesamoRaffMin, SesamoRaffMax,
                NocciolaMin, NocciolaMax, OlivaMin, OlivaMax, BurroCacaoMin, BurroCacaoMax, BabassuMin, BabassuMax,
                KariteMin, KariteMax, BurroMin, BurroMax, StruttoMin, StruttoMax, SegoRafMin, SegoRafMax,
				XMin, XMax, YMin, YMax, ZMin, ZMax;

		//true se devo cercare quel grasso
        private bool PalmaRafPres, PalmaAfrPres, PalmaOle60Pres, PalmaOle62Pres, PalmaOle64Pres, PalmaSt48Pres, PalmaSt53Pres,
            CoccoRafPres, CoccoIdrPres, PalmistoRPres, PalmistoIPres, PalmistoStPres, PalmistoFrazPres, SoiaRafPres,
            ColzaRafPres, ArachideRPres, VinaccioloPres, MaisRafPres, GirasoleALinoPres, GirasoleAOlePres, SesamoRaffPres,
            NocciolaPres, OlivaPres, BurroCacaoPres, BabassuPres, KaritePres, BurroPres, StruttoPres, SegoRafPres,
			XPres, YPres, ZPres,
            //forza refresh video
            frzRefresh;
		
        // inizializzazione form
        public Elaborazione(int vStepPercent,
            float vC4, float vC6, float vC8, float vC10, float vC12, float vC14, float vC14m, float vC15, float vC16ISO,
            float vC16, float vC16m, float vC17ISO, float vC17, float vC17m,
            float vC18, float vC18ISO, float vC18m, float vC18mm, float vC18mmm, float vC18CON,
            float vC20, float vC20m, float vC22, float vC22m,
            int vPalmaRafMin, int vPalmaRafMax, int vPalmaAfrMin, int vPalmaAfrMax,
            int vPalmaOle60Min, int vPalmaOle60Max, int vPalmaOle62Min, int vPalmaOle62Max, int vPalmaOle64Min, int vPalmaOle64Max,
            int vPalmaSt48Min, int vPalmaSt48Max, int vPalmaSt53Min, int vPalmaSt53Max, int vCoccoRafMin, int vCoccoRafMax, int vCoccoIdrMin,
            int vCoccoIdrMax, int vPalmistoRMin, int vPalmistoRMax, int vPalmistoIMin, int vPalmistoIMax, int vPalmistoStMin,
            int vPalmistoStMax, int vPalmistoFrazMin, int vPalmistoFrazMax, int vSoiaRafMin, int vSoiaRafMax, int vColzaRafMin, int vColzaRafMax,
            int vArachideRMin, int vArachideRMax, int vVinaccioloMin, int vVinaccioloMax, int vMaisRafMin, int vMaisRafMax,
            int vGirasoleALinoMin, int vGirasoleALinoMax, int vGirasoleAOleMin, int vGirasoleAOleMax, int vSesamoRaffMin, int vSesamoRaffMax,
            int vNocciolaMin, int vNocciolaMax, int vOlivaMin, int vOlivaMax, int vBurroCacaoMin, int vBurroCacaoMax, int vBabassuMin, int vBabassuMax,
            int vKariteMin, int vKariteMax, int vBurroMin, int vBurroMax, int vStruttoMin, int vStruttoMax, int vSegoRafMin, int vSegoRafMax,
		    int vXMin, int vXMax, int vYMin, int vYMax, int vZMin, int vZMax,
            //standard
            float[] vstdPalmaRaf, float[] vstdPalmaAfr, float[] vstdPalmaOle60, float[] vstdPalmaOle62, float[] vstdPalmaOle64,
            float[] vstdPalmaSt48, float[] vstdPalmaSt53, float[] vstdCoccoRaf, float[] vstdCoccoIdr,float[] vstdPalmistoR,
            float[] vstdPalmistoI, float[] vstdPalmistoSt,float[] vstdPalmistoFraz, float[] vstdSoiaRaf, float[] vstdColzaRaf,
            float[] vstdArachideR, float[] vstdVinacciolo, float[] vstdMaisRaf, float[] vstdGirasoleALino, float[] vstdGirasoleAOle,
            float[] vstdSesamoRaff, float[] vstdNocciola, float[] vstdOliva, float[] vstdBurroCacao, float[] vstdBabassu, float[] vstdKarite,
            float[] vstdBurro, float[] vstdStrutto, float[] vstdSegoRaf, float[] vstdX, float[] vstdY, float[] vstdZ)
        {
            StepPercent = vStepPercent;
            AcidiGrassiRic[0] = vC4;
            AcidiGrassiRic[1] = vC6;
            AcidiGrassiRic[2] = vC8;
            AcidiGrassiRic[3] = vC10;
            AcidiGrassiRic[4] = vC12;
            AcidiGrassiRic[5] = vC14;
            AcidiGrassiRic[6] = vC14m;
            AcidiGrassiRic[7] = vC15;
            AcidiGrassiRic[8] = vC16ISO;
            AcidiGrassiRic[9] = vC16;
            AcidiGrassiRic[10] = vC16m;
            AcidiGrassiRic[11] = vC17ISO;
            AcidiGrassiRic[12] = vC17;
            AcidiGrassiRic[13] = vC17m;
            AcidiGrassiRic[14] = vC18;
            AcidiGrassiRic[15] = vC18ISO;
            AcidiGrassiRic[16] = vC18m;
            AcidiGrassiRic[17] = vC18mm;
            AcidiGrassiRic[18] = vC18mmm;
            AcidiGrassiRic[19] = vC18CON;
            AcidiGrassiRic[20] = vC20;
            AcidiGrassiRic[21] = vC20m;
            AcidiGrassiRic[22] = vC22;
            AcidiGrassiRic[23] = vC22m;
            
            //assegna range di ricerca
            PalmaRafMin = vPalmaRafMin;
            PalmaRafMax = vPalmaRafMax;
            PalmaAfrMin = vPalmaAfrMin;
            PalmaAfrMax = vPalmaAfrMax;
            PalmaOle60Min = vPalmaOle60Min;
            PalmaOle60Max = vPalmaOle60Max;
            PalmaOle62Min = vPalmaOle62Min;
            PalmaOle62Max = vPalmaOle62Max;
            PalmaOle64Min = vPalmaOle64Min;
            PalmaOle64Max = vPalmaOle64Max;
            PalmaSt48Min = vPalmaSt48Min;
            PalmaSt48Max = vPalmaSt48Max;
            PalmaSt53Min = vPalmaSt53Min;
            PalmaSt53Max = vPalmaSt53Max;
            CoccoRafMin = vCoccoRafMin;
            CoccoRafMax = vCoccoRafMax;
            CoccoIdrMin = vCoccoIdrMin;
            CoccoIdrMax = vCoccoIdrMax;
            PalmistoRMin = vPalmistoRMin;
            PalmistoRMax = vPalmistoRMax;
            PalmistoIMin = vPalmistoIMin;
            PalmistoIMax = vPalmistoIMax;
            PalmistoStMin = vPalmistoStMin;
            PalmistoStMax = vPalmistoStMax;
            PalmistoFrazMin = vPalmistoFrazMin;
            PalmistoFrazMax = vPalmistoFrazMax;
            SoiaRafMin = vSoiaRafMin;
            SoiaRafMax = vSoiaRafMax;
            ColzaRafMin = vColzaRafMin;
            ColzaRafMax = vColzaRafMax;
            ArachideRMin = vArachideRMin;
            ArachideRMax = vArachideRMax;
            VinaccioloMin = vVinaccioloMin;
            VinaccioloMax = vVinaccioloMax;
            MaisRafMin = vMaisRafMin;
            MaisRafMax = vMaisRafMax;
            GirasoleALinoMin = vGirasoleALinoMin;
            GirasoleALinoMax = vGirasoleALinoMax;
            GirasoleAOleMin = vGirasoleAOleMin;
            GirasoleAOleMax = vGirasoleAOleMax;
            SesamoRaffMin = vSesamoRaffMin;
            SesamoRaffMax = vSesamoRaffMax;
            NocciolaMin = vNocciolaMin;
            NocciolaMax = vNocciolaMax;
            OlivaMin = vOlivaMin;
            OlivaMax = vOlivaMax;
            BurroCacaoMin = vBurroCacaoMin;
            BurroCacaoMax = vBurroCacaoMax;
            BabassuMin = vBabassuMin;
            BabassuMax = vBabassuMax;
            KariteMin = vKariteMin;
            KariteMax = vKariteMax;
            BurroMin = vBurroMin;
            BurroMax = vBurroMax;
            StruttoMin = vStruttoMin;
            StruttoMax = vStruttoMax;
            SegoRafMin = vSegoRafMin;
            SegoRafMax = vSegoRafMax;
            XMin = vXMin;
			XMax = vXMax;
			YMin = vYMin;
			YMax = vYMax;
			ZMin = vZMin;
			ZMax = vZMax;
			
            InitializeComponent();

            //assegnamento standard
            stdPalmaRaf = vstdPalmaRaf;
            stdPalmaAfr = vstdPalmaAfr;
            stdPalmaOle60 = vstdPalmaOle60;
            stdPalmaOle62 = vstdPalmaOle62;
            stdPalmaOle64 = vstdPalmaOle64;
            stdPalmaSt48 = vstdPalmaSt48;
            stdPalmaSt53 = vstdPalmaSt53;
            stdCoccoRaf = vstdCoccoRaf;
            stdCoccoIdr = vstdCoccoIdr;
            stdPalmistoR = vstdPalmistoR;
            stdPalmistoI = vstdPalmistoI;
            stdPalmistoSt = vstdPalmistoSt;
            stdPalmistoFraz = vstdPalmistoFraz;
            stdSoiaRaf = vstdSoiaRaf;
            stdColzaRaf = vstdColzaRaf;
            stdArachideR = vstdArachideR;
            stdVinacciolo = vstdVinacciolo;
            stdMaisRaf = vstdMaisRaf;
            stdGirasoleALino = vstdGirasoleALino;
            stdGirasoleAOle = vstdGirasoleAOle;
            stdSesamoRaff = vstdSesamoRaff;
            stdNocciola = vstdNocciola;
            stdOliva = vstdOliva;
            stdBurroCacao = vstdBurroCacao;
            stdBabassu = vstdBabassu;
            stdKarite = vstdKarite;
            stdBurro = vstdBurro;
            stdStrutto = vstdStrutto;
            stdSegoRaf = vstdSegoRaf;
			stdX = vstdX;
			stdY = vstdY;
			stdZ = vstdZ;
			
			GrassDaElab=0;
			TentativiMax=1;

			if (PalmaRafMax>0) {
                TentativiMax *= ((PalmaRafMax - PalmaRafMin) / StepPercent) + 1;
				PalmaRafPres=true;
				GrassDaElab++;
			}
            if (PalmaAfrMax>0) {
                TentativiMax *= ((PalmaAfrMax - PalmaAfrMin) / StepPercent) + 1;
				PalmaAfrPres=true;
				GrassDaElab++;
			}
            if (PalmaOle60Max>0) {
                TentativiMax *= ((PalmaOle60Max - PalmaOle60Min) / StepPercent) + 1;
				PalmaOle60Pres=true;
				GrassDaElab++;
			}
            if (PalmaOle62Max > 0)
            {
                TentativiMax *= ((PalmaOle62Max - PalmaOle62Min) / StepPercent) + 1;
                PalmaOle62Pres = true;
                GrassDaElab++;
            }
            if (PalmaOle64Max > 0)
            {
                TentativiMax *= ((PalmaOle64Max - PalmaOle64Min) / StepPercent) + 1;
                PalmaOle64Pres = true;
                GrassDaElab++;
            }
            if (PalmaSt48Max>0) {
                TentativiMax *= ((PalmaSt48Max - PalmaSt48Min) / StepPercent) + 1;
				PalmaSt48Pres=true;
				GrassDaElab++;
			}
            if (PalmaSt53Max > 0)
            {
                TentativiMax *= ((PalmaSt53Max - PalmaSt53Min) / StepPercent) + 1;
                PalmaSt53Pres = true;
                GrassDaElab++;
            }
            if (CoccoRafMax>0) {
                TentativiMax *= ((CoccoRafMax - CoccoRafMin) / StepPercent) + 1;
				CoccoRafPres=true;
				GrassDaElab++;
			}
            if (CoccoIdrMax>0) {
                TentativiMax *= ((CoccoIdrMax - CoccoIdrMin) / StepPercent) + 1;
				CoccoIdrPres=true;
				GrassDaElab++;
			}
            if (PalmistoRMax>0) {
                TentativiMax *= ((PalmistoRMax - PalmistoRMin) / StepPercent) + 1;
				PalmistoRPres=true;
				GrassDaElab++;
			}
            if (PalmistoIMax>0) {
                TentativiMax *= ((PalmistoIMax - PalmistoIMin) / StepPercent) + 1;
				PalmistoIPres=true;
				GrassDaElab++;
			}
            if (PalmistoStMax>0) {
				TentativiMax *= ((PalmistoStMax-PalmistoStMin)/StepPercent)+1;
				PalmistoStPres=true;
				GrassDaElab++;
			}
            if (PalmistoFrazMax > 0)
            {
                TentativiMax *= ((PalmistoFrazMax - PalmistoFrazMin) / StepPercent) + 1;
                PalmistoFrazPres = true;
                GrassDaElab++;
            }
            if (SoiaRafMax>0) {
				TentativiMax *= ((SoiaRafMax-SoiaRafMin)/StepPercent)+1;
				SoiaRafPres=true;
				GrassDaElab++;
			}
            if (ColzaRafMax > 0)
            {
                TentativiMax *= ((ColzaRafMax - ColzaRafMin) / StepPercent) + 1;
                ColzaRafPres = true;
                GrassDaElab++;
            }
            if (ArachideRMax > 0)
            {
                TentativiMax *= ((ArachideRMax - ArachideRMin) / StepPercent) + 1;
                ArachideRPres = true;
                GrassDaElab++;
            }
            if (VinaccioloMax > 0)
            {
                TentativiMax *= ((VinaccioloMax - VinaccioloMin) / StepPercent) + 1;
                VinaccioloPres = true;
                GrassDaElab++;
            }
            if (MaisRafMax > 0)
            {
                TentativiMax *= ((MaisRafMax - MaisRafMin) / StepPercent) + 1;
                MaisRafPres = true;
                GrassDaElab++;
            }
            if (GirasoleALinoMax > 0)
            {
                TentativiMax *= ((GirasoleALinoMax - GirasoleALinoMin) / StepPercent) + 1;
                GirasoleALinoPres = true;
                GrassDaElab++;
            }
            if (GirasoleAOleMax > 0)
            {
                TentativiMax *= ((GirasoleAOleMax - GirasoleAOleMin) / StepPercent) + 1;
                GirasoleAOlePres = true;
                GrassDaElab++;
            }
            if (SesamoRaffMax > 0)
            {
                TentativiMax *= ((SesamoRaffMax - SesamoRaffMin) / StepPercent) + 1;
                SesamoRaffPres = true;
                GrassDaElab++;
            }
            if (NocciolaMax>0) {
				TentativiMax *= ((NocciolaMax-NocciolaMin)/StepPercent)+1;
				NocciolaPres=true;
				GrassDaElab++;
			}
            if (OlivaMax>0) {
				TentativiMax *= ((OlivaMax-OlivaMin)/StepPercent)+1;
				OlivaPres=true;
				GrassDaElab++;
			}
            if (BurroCacaoMax>0) {
				TentativiMax *= ((BurroCacaoMax-BurroCacaoMin)/StepPercent)+1;
				BurroCacaoPres=true;
				GrassDaElab++;
			}
            if (BabassuMax > 0)
            {
                TentativiMax *= ((BabassuMax - BabassuMin) / StepPercent) + 1;
                BabassuPres = true;
                GrassDaElab++;
            }
            if (KariteMax > 0)
            {
                TentativiMax *= ((KariteMax - KariteMin) / StepPercent) + 1;
                KaritePres = true;
                GrassDaElab++;
            }
            if (BurroMax > 0)
            {
                TentativiMax *= ((BurroMax - BurroMin) / StepPercent) + 1;
                BurroPres = true;
                GrassDaElab++;
            }
            if (StruttoMax>0) {
				TentativiMax *= ((StruttoMax-StruttoMin)/StepPercent)+1;
				StruttoPres=true;
				GrassDaElab++;
			}
            if (SegoRafMax>0) {
				TentativiMax *= ((SegoRafMax-SegoRafMin)/StepPercent)+1;
				SegoRafPres=true;
				GrassDaElab++;
			}
			if (XMax>0) {
				TentativiMax *= ((XMax-XMin)/StepPercent)+1;
				XPres=true;
				GrassDaElab++;
			}
			if (YMax>0) {
				TentativiMax *= ((YMax-YMin)/StepPercent)+1;
				YPres=true;
				GrassDaElab++;
			}
			if (ZMax>0) {
				TentativiMax *= ((ZMax-ZMin)/StepPercent)+1;
				ZPres=true;
				GrassDaElab++;
			}
            
            prgBarAv.Minimum = 0;
            prgBarAv.Maximum = 100;
            //this.DoubleBuffered = true; non serve piu con il background worker
            
			//permette ad un thread di accedere ad oggetti di un altro thread
			//Control.CheckForIllegalCrossThreadCalls = false;
        }

        //routine iniziale, lancia elaborazione
        private void Elaborazione_Shown(object sender, EventArgs e)
        {
            //calcoli
            
            NumCicli = 0;
            NumCfr = 0;
            cntRefresh = 0;
            frzRefresh = true;
            IndApprOtt = int.MaxValue; //imposta 100, valore piu sbagliato possibile.
            startTime = DateTime.Now;
            
            //subPalmaRaf();
            //System.Threading.ThreadPool.SetMaxThreads(10,10);
            //bwMulo = new Thread(new ThreadStart(this.subPalmaRaf));
            
            //bwMulo.Priority = ThreadPriority.Highest;
            //bwMulo.SetApartmentState(ApartmentState.MTA);
            //bwMulo.Start();

            bwMulo = new BackgroundWorker();
            bwMulo.DoWork += new DoWorkEventHandler(subPalmaRaf);
            bwMulo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(ThreadFinished);
            bwMulo.WorkerSupportsCancellation = true;
            bwMulo.RunWorkerAsync();

        }

        //queste sub passano alla successiva se il loro Max è zero
        //se non è zero ciclano tra Min e Max ed a ogni ciclo chiamano la successiva

        //palma raff
        void subPalmaRaf(object sender, DoWorkEventArgs e)
        {
            
            if (PalmaRafPres)
            {
                for (iPalmaRaf = PalmaRafMin; iPalmaRaf <= PalmaRafMax; iPalmaRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmaAfr();
                }

            }
            else
            {
                subPalmaAfr();
            }
        }

        //palma afr.
        private void subPalmaAfr()
        {
            if (PalmaAfrPres)
            {
                for (iPalmaAfr = PalmaAfrMin; iPalmaAfr <= PalmaAfrMax; iPalmaAfr += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmaOle60();
                }

            }
            else
            {
                subPalmaOle60();
            }
        }

        //palma ol.
        private void subPalmaOle60()
        {
            if (PalmaOle60Pres)
            {
                for (iPalmaOle60 = PalmaOle60Min; iPalmaOle60 <= PalmaOle60Max; iPalmaOle60 += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmaOle62();
                }

            }
            else
            {
                subPalmaOle62();
            }
        }

        //palma ol.
        private void subPalmaOle62()
        {
            if (PalmaOle62Pres)
            {
                for (iPalmaOle62 = PalmaOle62Min; iPalmaOle62 <= PalmaOle62Max; iPalmaOle62 += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmaOle64();
                }

            }
            else
            {
                subPalmaOle64();
            }
        }

        //palma ol.
        private void subPalmaOle64()
        {
            if (PalmaOle64Pres)
            {
                for (iPalmaOle64 = PalmaOle64Min; iPalmaOle64 <= PalmaOle64Max; iPalmaOle64 += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmaSt48();
                }

            }
            else
            {
                subPalmaSt48();
            }
        }
        //palma st.
        private void subPalmaSt48()
        {
            if (PalmaSt48Pres)
            {
                for (iPalmaSt48 = PalmaSt48Min; iPalmaSt48 <= PalmaSt48Max; iPalmaSt48 += StepPercent)
                {
                    subPalmaSt53();
                }

            }
            else
            {
                subPalmaSt53();
            }
        }

        //palma st.
        private void subPalmaSt53()
        {
            if (PalmaSt53Pres)
            {
                for (iPalmaSt53 = PalmaSt53Min; iPalmaSt53 <= PalmaSt53Max; iPalmaSt53 += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subCoccoRaf();
                }

            }
            else
            {
                subCoccoRaf();
            }
        }

        //cocco raffinato
        private void subCoccoRaf()
        {
            if (CoccoRafPres)
            {
                for (iCoccoRaf = CoccoRafMin; iCoccoRaf <= CoccoRafMax; iCoccoRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subCoccoId();
                }

            }
            else
            {
                subCoccoId();
            }
        }

        //cocco idrogenato
        private void subCoccoId()
        {
            if (CoccoIdrPres)
            {
                for (iCoccoIdr = CoccoIdrMin; iCoccoIdr <= CoccoIdrMax; iCoccoIdr += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmistoR();
                }

            }
            else
            {
                subPalmistoR();
            }
        }

        //palmisto raffinato
        private void subPalmistoR()
        {
            if (PalmistoRPres)
            {
                for (iPalmistoR = PalmistoRMin; iPalmistoR <= PalmistoRMax; iPalmistoR += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmistoI();
                }

            }
            else
            {
                subPalmistoI();
            }
        }

        //palmisto idrogenato
        private void subPalmistoI()
        {
            if (PalmistoIPres)
            {
                for (iPalmistoI = PalmistoIMin; iPalmistoI <= PalmistoIMax; iPalmistoI += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmistoSt();
                }

            }
            else
            {
                subPalmistoSt();
            }
        }

        //palmisto st
        private void subPalmistoSt()
        {
            if (PalmistoStPres)
            {
                for (iPalmistoSt = PalmistoStMin; iPalmistoSt <= PalmistoStMax; iPalmistoSt += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subPalmistoFraz();
                }

            }
            else
            {
                subPalmistoFraz();
            }
        }

        //palmisto frazionato
        private void subPalmistoFraz()
        {
            if (PalmistoFrazPres)
            {
                for (iPalmistoFraz = PalmistoFrazMin; iPalmistoFraz <= PalmistoFrazMax; iPalmistoFraz += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subSoiaRaf();
                }

            }
            else
            {
                subSoiaRaf();
            }
        }

        //soia raffinato
        private void subSoiaRaf()
        {
            if (SoiaRafPres)
            {
                for (iSoiaRaf = SoiaRafMin; iSoiaRaf <= SoiaRafMax; iSoiaRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subColzaRaf();
                }

            }
            else
            {
                subColzaRaf();
            }
        }

        //colza raffinato
        private void subColzaRaf()
        {
            if (ColzaRafPres)
            {
                for (iColzaRaf = ColzaRafMin; iColzaRaf <= ColzaRafMax; iColzaRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subArachideR();
                }

            }
            else
            {
                subArachideR();
            }
        }

        //arachide raffinato
        private void subArachideR()
        {
            if (ArachideRPres)
            {
                for (iArachideR = ArachideRMin; iArachideR <= ArachideRMax; iArachideR += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subVinacciolo();
                }

            }
            else
            {
                subVinacciolo();
            }
        }

        //vinacciolo
        private void subVinacciolo()
        {
            if (VinaccioloPres)
            {
                for (iVinacciolo = VinaccioloMin; iVinacciolo <= VinaccioloMax; iVinacciolo += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subMaisRaf();
                }

            }
            else
            {
                subMaisRaf();
            }
        }

        //mais raffinato
        private void subMaisRaf()
        {
            if (MaisRafPres)
            {
                for (iMaisRaf = MaisRafMin; iMaisRaf <= MaisRafMax; iMaisRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subGirasoleALino();
                }

            }
            else
            {
                subGirasoleALino();
            }
        }

        //girasole
        private void subGirasoleALino()
        {
            if (GirasoleALinoPres)
            {
                for (iGirasoleALino = GirasoleALinoMin; iGirasoleALino <= GirasoleALinoMax; iGirasoleALino += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subGirasoleAOle();
                }

            }
            else
            {
                subGirasoleAOle();
            }
        }

        //girasole
        private void subGirasoleAOle()
        {
            if (GirasoleAOlePres)
            {
                for (iGirasoleAOle = GirasoleAOleMin; iGirasoleAOle <= GirasoleAOleMax; iGirasoleAOle += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subSesamoRaf();
                }

            }
            else
            {
                subSesamoRaf();
            }
        }

        //sesamo raffinato

        private void subSesamoRaf()
        {
            if (SesamoRaffPres)
            {
                for (iSesamoRaff = SesamoRaffMin; iSesamoRaff <= SesamoRaffMax; iSesamoRaff += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subNocciola();
                }

            }
            else
            {
                subNocciola();
            }
        }

        //nocciola
        private void subNocciola()
        {
            if (NocciolaPres)
            {
                for (iNocciola = NocciolaMin; iNocciola <= NocciolaMax; iNocciola += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subOliva();
                }

            }
            else
            {
                subOliva();
            }
        }

        //oliva
        private void subOliva()
        {
            if (OlivaPres)
            {
                for (iOliva = OlivaMin; iOliva <= OlivaMax; iOliva += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subBurroCacao();
                }

            }
            else
            {
                subBurroCacao();
            }
        }

        //burro cacao
        private void subBurroCacao()
        {
            if (BurroCacaoPres)
            {
                for (iBurroCacao = BurroCacaoMin; iBurroCacao <= BurroCacaoMax; iBurroCacao += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subBabassu();
                }

            }
            else
            {
                subBabassu();
            }
        }

        //babassu
        private void subBabassu()
        {
            if (BabassuPres)
            {
                for (iBabassu = BabassuMin; iBabassu <= BabassuMax; iBabassu += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subKarite();
                }

            }
            else
            {
                subKarite();
            }
        }

        //karite
        private void subKarite()
        {
            if (KaritePres)
            {
                for (iKarite = KariteMin; iKarite <= KariteMax; iKarite += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subBurro();
                }

            }
            else
            {
                subBurro();
            }
        }

        //burro
        private void subBurro()
        {
            if (BurroPres)
            {
                for (iBurro = BurroMin; iBurro <= BurroMax; iBurro += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subStrutto();
                }

            }
            else
            {
                subStrutto();
            }
        }
        //strutto
        private void subStrutto()
        {
            if (StruttoPres)
            {
                for (iStrutto = StruttoMin; iStrutto <= StruttoMax; iStrutto += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subSegoRaf();
                }

            }
            else
            {
                subSegoRaf();
            }
        }

        //sego raffinato
        private void subSegoRaf()
        {
            if (SegoRafPres)
            {
                for (iSegoRaf = SegoRafMin; iSegoRaf <= SegoRafMax; iSegoRaf += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subX();
                }

            }
            else
            {
                subX();
            }
        }
		
		//personalizzato X
		private void subX()
        {
            if (XPres)
            {
                for (iX = XMin; iX <= XMax; iX += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subY();
                }

            }
            else
            {
                subY();
            }
        }

		//personalizzato Y
		private void subY()
        {
            if (YPres)
            {
                for (iY = YMin; iY <= YMax; iY += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    subZ();
                }

            }
            else
            {
                subZ();
            }
        }

		//personalizzato Z
		private void subZ()
        {
            if (ZPres)
            {
                for (iZ = ZMin; iZ <= ZMax; iZ += StepPercent)
                {
                    if (bwMulo.CancellationPending) break;
                    funcCalcoli();
                }

            }
            else
            {
                funcCalcoli();
            }
        }

        //i calcoli veri e propri
        private void funcCalcoli()
        {
            float Tot;
            float[] AcidiGrassi=new float[NUM_ACIDI_GRASSI];
            int Ind,ProgressPercent;

            Tot = iPalmaRaf + iPalmaAfr + iPalmaOle60 + iPalmaOle62 + iPalmaOle64 + iPalmaSt48 + iPalmaSt53 +
            iCoccoRaf + iCoccoIdr + iPalmistoR + iPalmistoI + iPalmistoSt + iPalmistoFraz + iSoiaRaf +
            iColzaRaf + iArachideR + iVinacciolo + iMaisRaf + iGirasoleALino + iGirasoleAOle + iSesamoRaff +
            iNocciola + iOliva + iBurroCacao + iBabassu + iKarite + iBurro + iStrutto + iSegoRaf + iX +iY +iZ;
            
            if (Tot == 100) //se totale = 100 procede ad esaminare la formula
            {
                NumCfr++;
                IndApprRel = 0;
                for (Ind = 0; Ind < NUM_ACIDI_GRASSI; Ind++)
                {
                    AcidiGrassi[Ind] = (stdPalmaRaf[Ind] * iPalmaRaf +
                        stdPalmaAfr[Ind] * iPalmaAfr +
                        stdPalmaOle60[Ind] * iPalmaOle60 +
                        stdPalmaOle62[Ind] * iPalmaOle62 +
                        stdPalmaOle64[Ind] * iPalmaOle64 +
                        stdPalmaSt48[Ind] * iPalmaSt48 +
                        stdPalmaSt53[Ind] * iPalmaSt53 +
                        stdCoccoRaf[Ind] * iCoccoRaf +
                        stdCoccoIdr[Ind] * iCoccoIdr +
                        stdPalmistoR[Ind] * iPalmistoR +
                        stdPalmistoI[Ind] * iPalmistoI +
                        stdPalmistoSt[Ind] * iPalmistoSt +
                        stdPalmistoFraz[Ind] * iPalmistoFraz +
                        stdSoiaRaf[Ind] * iSoiaRaf +
                        stdColzaRaf[Ind] * iColzaRaf +
                        stdArachideR[Ind] * iArachideR +
                        stdVinacciolo[Ind] * iVinacciolo +
                        stdMaisRaf[Ind] * iMaisRaf +
                        stdGirasoleALino[Ind] * iGirasoleALino +
                        stdGirasoleAOle[Ind] * iGirasoleAOle +
                        stdSesamoRaff[Ind] * iSesamoRaff +
                        stdNocciola[Ind] * iNocciola +
                        stdOliva[Ind] * iOliva +
                        stdBurroCacao[Ind] * iBurroCacao +
                        stdBabassu[Ind] * iBabassu +
                        stdKarite[Ind] * iKarite +
                        stdBurro[Ind] * iBurro +
                        stdStrutto[Ind] * iStrutto +
                        stdSegoRaf[Ind] * iSegoRaf +
					    stdX[Ind] * iX +
					    stdY[Ind] * iY +
					    stdZ[Ind] * iZ) / 100;
                    IndApprRel += Math.Abs(AcidiGrassiRic[Ind] - AcidiGrassi[Ind]);
                    //SetControlPropertyThreadSafe(lblC22mG, "Text", AcidiGrassi[23].ToString());
                    //Application.DoEvents();
                    //MessageBox.Show(AcidiGrassiRic[Ind].ToString() + "-" + AcidiGrassi[Ind].ToString() + " indapprel: " + IndApprRel.ToString());
                }
                //IndApprRel /= 100;

                //se aprrossimazione relativa inferiore di ottimale...
                if (IndApprRel<IndApprOtt){
                    IndApprOtt = IndApprRel;

                    oPalmaRaf = iPalmaRaf;
                    oPalmaAfr = iPalmaAfr;
                    oPalmaOle60 = iPalmaOle60;
                    oPalmaOle62 = iPalmaOle62;
                    oPalmaOle64 = iPalmaOle64;
                    oPalmaSt48 = iPalmaSt48;
                    oPalmaSt53 = iPalmaSt53;
                    oCoccoRaf = iCoccoRaf;
                    oCoccoIdr = iCoccoIdr;
                    oPalmistoR = iPalmistoR;
                    oPalmistoI = iPalmistoI;
                    oPalmistoSt = iPalmistoSt;
                    oPalmistoFraz = iPalmistoFraz;
                    oSoiaRaf = iSoiaRaf;
                    oColzaRaf = iColzaRaf;
                    oArachideR = iArachideR;
                    oVinacciolo = iVinacciolo;
                    oMaisRaf = iMaisRaf;
                    oGirasoleALino = iGirasoleALino;
                    oGirasoleAOle = iGirasoleAOle;
                    oSesamoRaff = iSesamoRaff;
                    oNocciola = iNocciola;
                    oOliva = iOliva;
                    oBurroCacao = iBurroCacao;
                    oBabassu = iBabassu;
                    oKarite = iKarite;
                    oBurro = iBurro;
                    oStrutto = iStrutto;
                    oSegoRaf =iSegoRaf;
                    oX = iX;
					oY = iY;
					oZ = iZ;
					
                    //imposta gli acidi grassi ottimali
                    for (Ind = 0; Ind < NUM_ACIDI_GRASSI; Ind++)
                    {
                        AcidiGrassiOtt[Ind] = AcidiGrassi[Ind];
                    }

                    //calcolo del numero di iodio
                    oC16mIODIO = AcidiGrassiOtt[10] * IODIO_C16M;
                    oC17mIODIO = AcidiGrassiOtt[13] * IODIO_C17M;
                    oC18ISOIODIO = AcidiGrassiOtt[15] * IODIO_C18ISO;
                    oC18mIODIO = AcidiGrassiOtt[16] * IODIO_C18M;
                    oC18mmIODIO = AcidiGrassiOtt[17] * IODIO_C18MM;
                    oC18mmmIODIO = AcidiGrassiOtt[18] * IODIO_C18MMM;
                    oC20mIODIO = AcidiGrassiOtt[21] * IODIO_C20M;
                    oC22mIODIO = AcidiGrassiOtt[23] * IODIO_C22M;

                    oNIodio = (oC16mIODIO + oC17mIODIO + oC18ISOIODIO + oC18mIODIO + oC18mmIODIO +
                        oC18mmmIODIO + oC20mIODIO + oC22mIODIO) / 100;
                    oNIodio = oNIodio - (oNIodio * 5 / 100);

                    //imposta a video il numero di iodio
                    SetControlPropertyThreadSafe(lblNIodio, "Text", oNIodio.ToString("F1"));

                    //imposta a video i valori migliori dei grassi
                    SetControlPropertyThreadSafe(lblPalmaRaffV, "Text", oPalmaRaf.ToString());
                    SetControlPropertyThreadSafe(lblPalmaAfrV, "Text", oPalmaAfr.ToString());
                    SetControlPropertyThreadSafe(lblPalmaOle60V, "Text", oPalmaOle60.ToString());
                    SetControlPropertyThreadSafe(lblPalmaOle62V, "Text", oPalmaOle62.ToString());
                    SetControlPropertyThreadSafe(lblPalmaOle64V, "Text", oPalmaOle64.ToString());
                    SetControlPropertyThreadSafe(lblPalmaSt48V, "Text", oPalmaSt48.ToString());
                    SetControlPropertyThreadSafe(lblPalmaSt53V, "Text", oPalmaSt53.ToString());
                    SetControlPropertyThreadSafe(lblCoccoRaffV, "Text", oCoccoRaf.ToString());
                    SetControlPropertyThreadSafe(lblCoccoidrV, "Text", oCoccoIdr.ToString());
                    SetControlPropertyThreadSafe(lblPalmistoRV, "Text", oPalmistoR.ToString());
                    SetControlPropertyThreadSafe(lblPalmistoIV, "Text", oPalmistoI.ToString());
                    SetControlPropertyThreadSafe(lblPalmistoSTV, "Text", oPalmistoSt.ToString());
                    SetControlPropertyThreadSafe(lblPalmistoFrazV, "Text", oPalmistoFraz.ToString());
                    SetControlPropertyThreadSafe(lblSoiaRaffV, "Text", oSoiaRaf.ToString());
                    SetControlPropertyThreadSafe(lblColzaRaffV, "Text", oColzaRaf.ToString());
                    SetControlPropertyThreadSafe(lblArachideRV, "Text", oArachideR.ToString());
                    SetControlPropertyThreadSafe(lblVinaccioloV, "Text", oVinacciolo.ToString());
                    SetControlPropertyThreadSafe(lblMaisRaffV, "Text", oMaisRaf.ToString());
                    SetControlPropertyThreadSafe(lblGirasoleALinoV, "Text", oGirasoleALino.ToString());
                    SetControlPropertyThreadSafe(lblGirasoleAOleV, "Text", oGirasoleAOle.ToString());
                    SetControlPropertyThreadSafe(lblSesamoRaffV, "Text", oSesamoRaff.ToString());
                    SetControlPropertyThreadSafe(lblNocciolaV, "Text", oNocciola.ToString());
                    SetControlPropertyThreadSafe(lblOlivaV, "Text", oOliva.ToString());
                    SetControlPropertyThreadSafe(lblBurroCacaoV, "Text", oBurroCacao.ToString());
                    SetControlPropertyThreadSafe(lblBabassuV, "Text", oBabassu.ToString());
                    SetControlPropertyThreadSafe(lblKariteV, "Text", oKarite.ToString());
                    SetControlPropertyThreadSafe(lblBrurroV, "Text", oBurro.ToString());
                    SetControlPropertyThreadSafe(lblStruttoV, "Text", oStrutto.ToString());
                    SetControlPropertyThreadSafe(lblSegoRaffV, "Text", oSegoRaf.ToString());
					SetControlPropertyThreadSafe(lblXV, "Text", oX.ToString());
					SetControlPropertyThreadSafe(lblYV, "Text", oY.ToString());
					SetControlPropertyThreadSafe(lblZV, "Text", oZ.ToString());
					
                    //imposta a video i valori migliori degli acidi grassi
                    SetControlPropertyThreadSafe(lblC4V, "Text", AcidiGrassiOtt[0].ToString());
                    SetControlPropertyThreadSafe(lblC6V, "Text", AcidiGrassiOtt[1].ToString());
                    SetControlPropertyThreadSafe(lblC8V, "Text", AcidiGrassiOtt[2].ToString());
                    SetControlPropertyThreadSafe(lblC10V, "Text", AcidiGrassiOtt[3].ToString());
                    SetControlPropertyThreadSafe(lblC12V, "Text", AcidiGrassiOtt[4].ToString());
                    SetControlPropertyThreadSafe(lblC14V, "Text", AcidiGrassiOtt[5].ToString());
                    SetControlPropertyThreadSafe(lblC14mV, "Text", AcidiGrassiOtt[6].ToString());
                    SetControlPropertyThreadSafe(lblC15V, "Text", AcidiGrassiOtt[7].ToString());
                    SetControlPropertyThreadSafe(lblC16ISOV, "Text", AcidiGrassiOtt[8].ToString());
                    SetControlPropertyThreadSafe(lblC16V, "Text", AcidiGrassiOtt[9].ToString());
                    SetControlPropertyThreadSafe(lblC16mV, "Text", AcidiGrassiOtt[10].ToString());
                    SetControlPropertyThreadSafe(lblC17ISOV, "Text", AcidiGrassiOtt[11].ToString());
                    SetControlPropertyThreadSafe(lblC17V, "Text", AcidiGrassiOtt[12].ToString());
                    SetControlPropertyThreadSafe(lblC17mV, "Text", AcidiGrassiOtt[13].ToString());
                    SetControlPropertyThreadSafe(lblC18V, "Text", AcidiGrassiOtt[14].ToString());
                    SetControlPropertyThreadSafe(lblC18ISOV, "Text", AcidiGrassiOtt[15].ToString());
                    SetControlPropertyThreadSafe(lblC18mV, "Text", AcidiGrassiOtt[16].ToString());
                    SetControlPropertyThreadSafe(lblC18mmV, "Text", AcidiGrassiOtt[17].ToString());
                    SetControlPropertyThreadSafe(lblC18mmmV, "Text", AcidiGrassiOtt[18].ToString());
                    SetControlPropertyThreadSafe(lblC18CONV, "Text", AcidiGrassiOtt[19].ToString());
                    SetControlPropertyThreadSafe(lblC20V, "Text", AcidiGrassiOtt[20].ToString());
                    SetControlPropertyThreadSafe(lblC20mV, "Text", AcidiGrassiOtt[21].ToString());
                    SetControlPropertyThreadSafe(lblC22V, "Text", AcidiGrassiOtt[22].ToString());
                    SetControlPropertyThreadSafe(lblC22mV, "Text", AcidiGrassiOtt[23].ToString());

                    //imposta a video il miglior indice di approssimazione trovato
                    SetControlPropertyThreadSafe(lblIndApprOttV, "Text", IndApprOtt.ToString());
                    frzRefresh = true;
                }
            }
            NumCicli++;
            ProgressPercent = (int)(100 * NumCicli / TentativiMax);

            cntRefresh++;
            if (NumCicli == TentativiMax)
            {
                //MessageBox.Show("ci siamo");
                frzRefresh = true;
            }

            if ((((int)(100 * cntRefresh / TentativiMax)) == 10) || (frzRefresh))
            {
                //MessageBox.Show(((int)(100 * cntRefresh / TentativiMax)).ToString());
                cntRefresh = 0;
                frzRefresh = false;

                //imposta a video progress bar e numero cicli
                SetControlPropertyThreadSafe(prgBarAv, "Value", ProgressPercent);
                SetControlPropertyThreadSafe(lblProgress, "Text", NumCicli.ToString() + "/" + TentativiMax.ToString("G"));

                //imposta a video valori temporanei dei grassi - 0 se non ci sono dati sufficienti ad avere una formula valida (TOT<100)
                SetControlPropertyThreadSafe(lblPalmaRaffG, "Text", iPalmaRaf.ToString());
                SetControlPropertyThreadSafe(lblPalmaAfrG, "Text", iPalmaAfr.ToString());
                SetControlPropertyThreadSafe(lblPalmaOle60G, "Text", iPalmaOle60.ToString());
                SetControlPropertyThreadSafe(lblPalmaOle62G, "Text", iPalmaOle62.ToString());
                SetControlPropertyThreadSafe(lblPalmaOle64G, "Text", iPalmaOle64.ToString());
                SetControlPropertyThreadSafe(lblPalmaSt48G, "Text", iPalmaSt48.ToString());
                SetControlPropertyThreadSafe(lblPalmaSt53G, "Text", iPalmaSt53.ToString());
                SetControlPropertyThreadSafe(lblCoccoRaffG, "Text", iCoccoRaf.ToString());
                SetControlPropertyThreadSafe(lblCoccoidrG, "Text", iCoccoIdr.ToString());
                SetControlPropertyThreadSafe(lblPalmistoRG, "Text", iPalmistoR.ToString());
                SetControlPropertyThreadSafe(lblPalmistoIG, "Text", iPalmistoI.ToString());
                SetControlPropertyThreadSafe(lblPalmistoSTG, "Text", iPalmistoSt.ToString());
                SetControlPropertyThreadSafe(lblPalmistoFrazG, "Text", iPalmistoFraz.ToString());
                SetControlPropertyThreadSafe(lblSoiaRaffG, "Text", iSoiaRaf.ToString());
                SetControlPropertyThreadSafe(lblColzaRaffG, "Text", iColzaRaf.ToString());
                SetControlPropertyThreadSafe(lblArachideRG, "Text", iArachideR.ToString());
                SetControlPropertyThreadSafe(lblVinaccioloG, "Text", iVinacciolo.ToString());
                SetControlPropertyThreadSafe(lblMaisRaffG, "Text", iMaisRaf.ToString());
                SetControlPropertyThreadSafe(lblGirasoleALinoG, "Text", iGirasoleALino.ToString());
                SetControlPropertyThreadSafe(lblGirasoleAOleG, "Text", iGirasoleAOle.ToString());
                SetControlPropertyThreadSafe(lblSesamoRaffG, "Text", iSesamoRaff.ToString());
                SetControlPropertyThreadSafe(lblNocciolaG, "Text", iNocciola.ToString());
                SetControlPropertyThreadSafe(lblOlivaG, "Text", iOliva.ToString());
                SetControlPropertyThreadSafe(lblBurroCacaoG, "Text", iBurroCacao.ToString());
                SetControlPropertyThreadSafe(lblBabassuG, "Text", iBabassu.ToString());
                SetControlPropertyThreadSafe(lblKariteG, "Text", iKarite.ToString());
                SetControlPropertyThreadSafe(lblBrurroG, "Text", iBurro.ToString());
                SetControlPropertyThreadSafe(lblStruttoG, "Text", iStrutto.ToString());
                SetControlPropertyThreadSafe(lblSegoRaffG, "Text", iSegoRaf.ToString());
				SetControlPropertyThreadSafe(lblXG, "Text", iX.ToString());
                SetControlPropertyThreadSafe(lblYG, "Text", iY.ToString());
                SetControlPropertyThreadSafe(lblZG, "Text", iZ.ToString());

                //imposta a video valori attuali degli acidi grassi
                SetControlPropertyThreadSafe(lblC4G, "Text", AcidiGrassi[0].ToString());
                SetControlPropertyThreadSafe(lblC6G, "Text", AcidiGrassi[1].ToString());
                SetControlPropertyThreadSafe(lblC8G, "Text", AcidiGrassi[2].ToString());
                SetControlPropertyThreadSafe(lblC10G, "Text", AcidiGrassi[3].ToString());
                SetControlPropertyThreadSafe(lblC12G, "Text", AcidiGrassi[4].ToString());
                SetControlPropertyThreadSafe(lblC14G, "Text", AcidiGrassi[5].ToString());
                SetControlPropertyThreadSafe(lblC14mG, "Text", AcidiGrassi[6].ToString());
                SetControlPropertyThreadSafe(lblC15G, "Text", AcidiGrassi[7].ToString());
                SetControlPropertyThreadSafe(lblC16ISOG, "Text", AcidiGrassi[8].ToString());
                SetControlPropertyThreadSafe(lblC16G, "Text", AcidiGrassi[9].ToString());
                SetControlPropertyThreadSafe(lblC16mG, "Text", AcidiGrassi[10].ToString());
                SetControlPropertyThreadSafe(lblC17ISOG, "Text", AcidiGrassi[11].ToString());
                SetControlPropertyThreadSafe(lblC17G, "Text", AcidiGrassi[12].ToString());
                SetControlPropertyThreadSafe(lblC17mG, "Text", AcidiGrassi[13].ToString());
                SetControlPropertyThreadSafe(lblC18G, "Text", AcidiGrassi[14].ToString());
                SetControlPropertyThreadSafe(lblC18ISOG, "Text", AcidiGrassi[15].ToString());
                SetControlPropertyThreadSafe(lblC18mG, "Text", AcidiGrassi[16].ToString());
                SetControlPropertyThreadSafe(lblC18mmG, "Text", AcidiGrassi[17].ToString());
                SetControlPropertyThreadSafe(lblC18mmmG, "Text", AcidiGrassi[18].ToString());
                SetControlPropertyThreadSafe(lblC18CONG, "Text", AcidiGrassi[19].ToString());
                SetControlPropertyThreadSafe(lblC20G, "Text", AcidiGrassi[20].ToString());
                SetControlPropertyThreadSafe(lblC20mG, "Text", AcidiGrassi[21].ToString());
                SetControlPropertyThreadSafe(lblC22G, "Text", AcidiGrassi[22].ToString());
                SetControlPropertyThreadSafe(lblC22mG, "Text", AcidiGrassi[23].ToString());
                SetControlPropertyThreadSafe(lblIndAppRelG, "Text", IndApprRel.ToString());
                /*
                if (iPalmaRaf > 0)
                {
                    MessageBox.Show(AcidiGrassi[0].ToString() + "-" + iPalmaRaf.ToString());
                }*/
                //numero di confronti
                SetControlPropertyThreadSafe(lblNConfrEffG, "Text", NumCfr.ToString());
                //Application.DoEvents();
                //Update();
            }
            //fine!
            /*
            if (NumCicli == TentativiMax)
            {
                stopTime = DateTime.Now;
                duration = stopTime - startTime;
                //SetControlPropertyThreadSafe(lblTempo, "Text", "Tempo impiegato: " + duration.ToString());
                MessageBox.Show("Elaborazione completata!\nTempo impiegato: " + duration.ToString());
            }
            */
        }

        private void Elaborazione_Load(object sender, EventArgs e)
        {
            label1.Text = "Miscelazione simulata dei grassi previsti con uno step del " + StepPercent.ToString() + "%";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        public static void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            //int wkThread, GUIThread;
            //ThreadPool.GetAvailableThreads(out wkThread,out GUIThread);
            //if (GUIThread > 0)
            //{
            if (control.InvokeRequired)
            {
                control.BeginInvoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                //control.Update();
            }
            else
            {
                control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                //control.Update();
                //Application.DoEvents();
            }
            //}
        }


        void ThreadFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            /* Safe to mess with Controls now (RunWorkerCompleted executes on main thread) */

            //Thread has finished so show finish form and close
            stopTime = DateTime.Now;
            duration = stopTime - startTime;
            //SetControlPropertyThreadSafe(lblTempo, "Text", "Tempo impiegato: " + duration.ToString());
            MessageBox.Show("Elaborazione completata!\nTempo impiegato: " + duration.ToString());

            Form frmReport = new Report(oPalmaRaf, oPalmaAfr, oPalmaOle60, oPalmaOle62, oPalmaOle64, oPalmaSt48, oPalmaSt53,
                oCoccoRaf, oCoccoIdr, oPalmistoR, oPalmistoI, oPalmistoSt, oPalmistoFraz, oSoiaRaf,
                oColzaRaf, oArachideR, oVinacciolo, oMaisRaf, oGirasoleALino, oGirasoleAOle, oSesamoRaff,
                oNocciola, oOliva, oBurroCacao, oBabassu, oKarite, oBurro, oStrutto, oSegoRaf, oX, oY, oZ,
			    AcidiGrassiOtt, oNIodio, IndApprOtt, AcidiGrassiRic);
            frmReport.Show();
            this.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bwMulo.IsBusy)
            {
                bwMulo.CancelAsync();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frzRefresh = true;
        }
    }
}