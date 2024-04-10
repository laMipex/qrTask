using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;
using IronBarCode;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Image = System.Drawing.Image;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Creed
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                string qrData = $"Uplatilac: {txtBoxUplatilac.Text}\n" +
                            $"Svrha Uplate: {txtBoxSvrha.Text}\n" +
                            $"Primalac: {txtBoxPrimalac.Text}\n" +
                            $"Sifra Placanja: {txtBoxSif.Text}\n" +
                            $"Valuta: {txtBoxValuta.Text}\n" +
                            $"Iznos: {txtBoxIznos.Text}\n" +
                            $"Racun Primalaca: {txtBoxPrimalac.Text}\n" +
                            $"Broj Modela: {txtBoxModel.Text}\n" +
                            $"Poziv na Broj: {txtBoxPozivBr.Text}";

                QRCodeWriter.CreateQrCode(qrData, 250, QRCodeWriter.QrErrorCorrectionLevel.Medium).SaveAsPng("code.png");
                pictureBox1.Image = Image.FromFile("C:\\Users\\Mipex\\Desktop\\Programiranje\\Task\\DrugiZadatak\\Creed\\bin\\Debug\\code.png");

                string srData = "K:PR|V:01|R:170005001700100043|N:Media Centar Studio|I:RSD 600,00|P:Kupac IPS|SF:221|S:Izrada i Stampa IPS kod nalepnica|RO:00-IPS-001";
                string[] strings = srData.Split('|');

                
                foreach (string s in strings)
                {
                    string[] keyValue = s.Split(':');
                    dic.Add(keyValue[0], keyValue[1]);
                }

                foreach (var s in dic)
                {
                    switch (s.Key)
                    {
                        case "P":
                            txtBoxUplatilac.Text = s.Value;
                            break;
                        case "S":
                            txtBoxSvrha.Text = s.Value;
                            break;
                        case "N":
                            txtBoxPrimalac.Text = s.Value;
                            break;
                        case "SF":
                            txtBoxSif.Text = s.Value;
                            break;
                        case "R":
                            txtBoxRacun.Text = s.Value;
                            break;
                        case "I":
                            string[] valuta = s.Value.Split(' ');
                            txtBoxValuta.Text = valuta[0];
                            txtBoxIznos.Text = valuta[1];
                            break;
                        case "RO":
                            string[] pBroj = s.Value.Split(new[] { '-' }, 2);
                            txtBoxModel.Text = pBroj[0];
                            txtBoxPozivBr.Text = pBroj[1];
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            

            Document doc = new Document(PageSize.A4.Rotate());
            string outputPath = Path.Combine(Application.StartupPath, "output.pdf");
            FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                doc.Open();
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("C:\\Users\\Mipex\\Desktop\\Programiranje\\Task\\DrugiZadatak\\Creed\\bin\\Debug\\uplatnica.png");
                img.ScaleAbsolute(750, 350);
                doc.Add(img);

                foreach (var s in dic)
                {
                    Paragraph paragraph = new Paragraph();

                    switch (s.Key)
                    {
                        case "P":
                            Phrase phraseP = new Phrase(s.Value);
                            PdfContentByte cbP = writer.DirectContent;
                            cbP.BeginText();
                            cbP.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cbP.SetTextMatrix(75, 470);
                            cbP.ShowText(s.Value);
                            cbP.EndText();
                            break;
                        case "S":
                            Phrase phraseS = new Phrase(s.Value);
                            PdfContentByte cbS = writer.DirectContent;
                            cbS.BeginText();
                            cbS.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cbS.SetTextMatrix(75, 395);
                            cbS.ShowText(s.Value);
                            cbS.EndText();
                            break;
                        case "N":
                            Phrase phraseN = new Phrase(s.Value);
                            PdfContentByte cbN = writer.DirectContent;
                            cbN.BeginText();
                            cbN.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cbN.SetTextMatrix(75, 315);
                            cbN.ShowText(s.Value);
                            cbN.EndText();
                            break;
                        case "SF":
                            Phrase phraseSF = new Phrase(s.Value);
                            PdfContentByte cbSF = writer.DirectContent;
                            cbSF.BeginText();
                            cbSF.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cbSF.SetTextMatrix(400, 470);
                            cbSF.ShowText(s.Value);
                            cbSF.EndText();
                            break;
                        case "R":
                            Phrase phraseR = new Phrase(s.Value);
                            PdfContentByte cbR = writer.DirectContent;
                            cbR.BeginText();
                            cbR.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cbR.SetTextMatrix(400, 417);
                            cbR.ShowText(s.Value);
                            cbR.EndText();
                            break;
                        case "I":
                            string[] valuta = s.Value.Split(' ');
                            string text1 = valuta[0];
                            string text2 = valuta[1];

                            PdfContentByte cb = writer.DirectContent;
                            cb.BeginText();
                            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cb.SetTextMatrix(475, 470);
                            cb.ShowText(text1);
                            cb.EndText();

                            PdfContentByte cb2 = writer.DirectContent;
                            cb2.BeginText();
                            cb2.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cb2.SetTextMatrix(575, 470);
                            cb2.ShowText(text2);
                            cb2.EndText();

                            break;
                        case "RO":
                            string[] pBroj = s.Value.Split(new[] { '-' }, 2);
                            string text3 = pBroj[0];
                            string text4 = pBroj[1];

                            PdfContentByte cb3 = writer.DirectContent;
                            cb3.BeginText();
                            cb3.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cb3.SetTextMatrix(403, 370);
                            cb3.ShowText(text3);
                            cb3.EndText();

                            PdfContentByte cb4 = writer.DirectContent;
                            cb4.BeginText();
                            cb4.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 12);
                            cb4.SetTextMatrix(490, 370);
                            cb4.ShowText(text4);
                            cb4.EndText();
                            break;
                        default:
                            break;
                    }
                    doc.Add(paragraph);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                doc.Close();
            }

            btnOpenPdf.Enabled = true;
            btnClear.Enabled = true;
            ReadOnly();
        }


        private void btnOpenPdf_Click(object sender, EventArgs e)
        {
            try
            {
                string pdfPath = "C:\\Users\\Mipex\\Desktop\\Programiranje\\Task\\DrugiZadatak\\Creed\\bin\\Debug\\output.pdf";

                if (System.IO.File.Exists(pdfPath))
                {
                    Process.Start(pdfPath);
                }
                else
                {
                    MessageBox.Show("PDF file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;

            txtBoxIznos.Clear();
            txtBoxModel.Clear();
            txtBoxPozivBr.Clear();
            txtBoxPrimalac.Clear();
            txtBoxRacun.Clear();
            txtBoxSif.Clear();
            txtBoxSvrha.Clear();
            txtBoxUplatilac.Clear();
            txtBoxValuta.Clear();

            btnOpenPdf.Enabled = false;
            btnClear.Enabled = false;
        }

        private void ReadOnly()
        {
            txtBoxIznos.ReadOnly = true;
            txtBoxModel.ReadOnly = true;
            txtBoxPozivBr.ReadOnly = true;
            txtBoxPrimalac.ReadOnly = true;
            txtBoxRacun.ReadOnly = true;
            txtBoxSif.ReadOnly = true;
            txtBoxSvrha.ReadOnly = true;
            txtBoxUplatilac.ReadOnly = true;
            txtBoxValuta.ReadOnly = true;
        }
    }
}
