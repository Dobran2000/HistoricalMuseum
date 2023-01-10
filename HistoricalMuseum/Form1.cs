using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;

namespace HistoricalMuseum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(@"C:\Users\Dobran\Desktop\Tema2-SBC\Bucovina.png");
            setareListaAdauga();
            setareListaContinente();
            setareTipExponant();
            setareListaVizualizare();
            setareListaTipPersoanaVizitator();

            groupboxExponat.Visible = true;
            groupBox3.Visible = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button5.Enabled = false;
        }
        public void setareListaContinente()
        {
            listaContinente.Items.Add("Africa");
            listaContinente.Items.Add("America de Nord");
            listaContinente.Items.Add("America de Sud");
            listaContinente.Items.Add("Antarctica");
            listaContinente.Items.Add("Asia");
            listaContinente.Items.Add("Australia");
            listaContinente.Items.Add("Europa");

            listaContinente.Text = "Europa";
        }
        public void setareTipExponant()
        {
            listTipExponat.Items.Add("Tablou");
            listTipExponat.Items.Add("Arma");
            listTipExponat.Items.Add("Statuie");
            listTipExponat.Items.Add("Coroana");
            listTipExponat.Items.Add("Vesminte");
            listTipExponat.Items.Add("Armura");
            listTipExponat.Items.Add("Bijuterii");
            listTipExponat.Items.Add("Vase");

            listTipExponat.Text = "Tablou";
        }
        public void setareListaTipPersoanaVizitator()
        {
            tipPersoanaVizitator.Items.Add("Adult");
            tipPersoanaVizitator.Items.Add("Copil");
            tipPersoanaVizitator.Items.Add("Student");
            tipPersoanaVizitator.Items.Add("Pensionar");

            tipPersoanaVizitator.Text = "Adult";
            pretBiletVizitator.Text = "20";
        }
        public void setareListaAdauga()
        {
            listaAdauga.Items.Add("Exponat");
            listaAdauga.Items.Add("Vizitator");

            listaAdauga.Text = "Exponat";
        }
        public void setareListaVizualizare()
        {
            vizualizareListe.Items.Add("Exponat");
            vizualizareListe.Items.Add("Vizitator");
            vizualizareListe.Items.Add("Paznic");

            vizualizareListe.Text = "Exponat";
        }
        private void Afisare(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");
            if (vizualizareListe.Text == "Exponat")
            {
                try
                {
                    XmlNodeList nodes = doc.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");

                    List<Exponat> exponate = new List<Exponat>();
                    foreach (XmlNode nod in nodes)
                    {
                        Exponat exponat = new Exponat();

                        exponat.NumeExponat = nod.SelectSingleNode("codExponat").InnerText;
                        exponat.TipExponat = nod.SelectSingleNode("detalii/tipExponat").InnerText;
                        exponat.AnProvenienta = nod.SelectSingleNode("detalii/anProvenienta").InnerText;
                        exponat.Varsta = Int32.Parse(nod.SelectSingleNode("detalii/varsta").InnerText);
                        exponat.Continent = nod.SelectSingleNode("detalii/continent").InnerText;


                        Vitrina vitrina = new Vitrina();
                        vitrina.Nr = Int32.Parse(nod.SelectSingleNode("vitrina/nr").InnerText);
                        vitrina.Suprafata = Int32.Parse(nod.SelectSingleNode("vitrina/suprafata").InnerText);
                        vitrina.Material = nod.SelectSingleNode("vitrina/material").InnerText;
                        exponat.Vitrina = vitrina;

                        Paznic paznic = new Paznic();
                        paznic.Varsta = Int32.Parse(nod.SelectSingleNode("paznic/varsta").InnerText);
                        paznic.Prenume = nod.SelectSingleNode("paznic/prenume").InnerText;
                        paznic.Nume = nod.SelectSingleNode("paznic/nume").InnerText;
                        exponat.Paznic = paznic;

                        exponat.DataSosireMuzeu = new DateTime();
                        int an = Int32.Parse(nod.SelectSingleNode("dataSosireMuzeu/anSosire").InnerText);
                        int luna = Int32.Parse(nod.SelectSingleNode("dataSosireMuzeu/lunaSosire").InnerText);
                        int zi = Int32.Parse(nod.SelectSingleNode("dataSosireMuzeu/ziSosire").InnerText);
                        exponat.DataSosireMuzeu = new DateTime(an, luna, zi);

                        exponate.Add(exponat);
                    }

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Nr", typeof(int));
                    dt.Columns.Add("NumeExponat", typeof(string));
                    dt.Columns.Add("TipExponat", typeof(string));
                    dt.Columns.Add("AnProvenienta", typeof(string));
                    dt.Columns.Add("DataSosireMuzeu", typeof(string));
                    dt.Columns.Add("Vitrina", typeof(int));
                    dt.Columns.Add("Paznic", typeof(string));
                    dt.Columns.Add("Varsta", typeof(int));
                    dt.Columns.Add("Continent", typeof(string));
                    dt.Clear();

                    int i = 0;
                    foreach (Exponat exp in exponate)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Nr"] = ++i;
                        dr["NumeExponat"] = exp.NumeExponat;
                        dr["TipExponat"] = exp.TipExponat;
                        dr["DataSosireMuzeu"] = exp.DataSosireMuzeu;
                        dr["AnProvenienta"] = exp.AnProvenienta;
                        dr["Vitrina"] = exp.Vitrina.Nr;
                        dr["Paznic"] = exp.Paznic.Nume + " " + exp.Paznic.Prenume;
                        dr["Varsta"] = exp.Varsta;
                        dr["Continent"] = exp.Continent;
                        dt.Rows.Add(dr);
                    }
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else if (vizualizareListe.Text == "Paznic")
            {
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat/paznic");
                List<Paznic> paznici = new List<Paznic>();
                foreach (XmlNode nod in nodes)
                {
                    Paznic paznic = new Paznic();
                    paznic.Nume = nod.SelectSingleNode("nume").InnerText;
                    paznic.Prenume = nod.SelectSingleNode("prenume").InnerText;
                    paznic.Varsta = Int32.Parse(nod.SelectSingleNode("varsta").InnerText);
                    paznici.Add(paznic);
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Nr", typeof(int));
                dt.Columns.Add("Nume", typeof(string));
                dt.Columns.Add("Prenume", typeof(string));
                dt.Columns.Add("Varsta", typeof(string));
                dt.Clear();

                int i = 0;
                foreach (Paznic ing in paznici)
                {
                    DataRow dr = dt.NewRow();
                    dr["Nr"] = ++i;
                    dr["Nume"] = ing.Nume;
                    dr["Prenume"] = ing.Prenume;
                    dr["Varsta"] = ing.Varsta;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;
            }
            else if (vizualizareListe.Text == "Vizitator")
            {
                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/historicalmuseum/vizitatori/vizitator");
                List<Vizitator> vizitatori = new List<Vizitator>();
                foreach (XmlNode nod in nodes)
                {
                    Vizitator vizitator = new Vizitator();
                    vizitator.Nume = nod.SelectSingleNode("nume").InnerText;
                    vizitator.Prenume = nod.SelectSingleNode("prenume").InnerText;
                    vizitator.Varsta = Int32.Parse(nod.SelectSingleNode("varsta").InnerText);
                    vizitator.Pret = Int32.Parse(nod.SelectSingleNode("bilet/pret").InnerText);
                    vizitator.tipPersoana = nod.SelectSingleNode("bilet/tipPersoana").InnerText;
                    vizitatori.Add(vizitator);
                }
                DataTable dt = new DataTable();
                dt.Columns.Add("Nr", typeof(int));
                dt.Columns.Add("Nume", typeof(string));
                dt.Columns.Add("Prenume", typeof(string));
                dt.Columns.Add("Varsta", typeof(int));
                dt.Columns.Add("Tip persoana", typeof(string));
                dt.Columns.Add("Pret", typeof(int));
                dt.Clear();

                int i = 0;
                foreach (Vizitator ing in vizitatori)
                {
                    DataRow dr = dt.NewRow();
                    dr["Nr"] = ++i;
                    dr["Nume"] = ing.Nume;
                    dr["Prenume"] = ing.Prenume;
                    dr["Varsta"] = ing.Varsta;
                    dr["Tip persoana"] = ing.tipPersoana;
                    dr["Pret"] = ing.Pret;
                    dt.Rows.Add(dr);
                }
                dataGridView1.DataSource = dt;


            }
        }

        private void AdaugaExponat(object sender, EventArgs e)
        {
            string fileName = @"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml";

            XDocument doc = XDocument.Load(fileName);

            if (listaAdauga.Text == "Exponat")
            {

                if (txtNumeExponat.Text == "" ||
                    txtVarstaExponat.Text == "" || txtValoareEstimata.Text == "" ||
                    txtAnProvenienta.Text == "" || txtGreutateaExponat.Text == "" ||
                    txtNrVitrinaExponat.Text == "" || txtSuprafataVitrinaExponat.Text == "" ||
                    txtMaterialVitrina.Text == "" || txtNumePaznic.Text == "" ||
                    txtPrenumePaznic.Text == "" || txtVarstaPaznic.Text == ""
                    )
                {
                    MessageBox.Show("Completati toate campurile!");
                    goto Outer;
                }
                try
                {
                    int nr = Int32.Parse(txtVarstaExponat.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru varsta.");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru varsta");
                    goto Outer;
                }

                try
                {
                    int nr = Int32.Parse(txtNrVitrinaExponat.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru vitrina.");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru vitrina");
                    goto Outer;
                }
                try
                {
                    int nr = Int32.Parse(txtSuprafataVitrinaExponat.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru suprafata.");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr intreg pentru suprafata");
                    goto Outer;
                }
                try
                {
                    int nr = Int32.Parse(txtVarstaPaznic.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru varsta paznic.");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru varsta paznic");
                    goto Outer;
                }

                try
                {
                    int nr = Int32.Parse(txtValoareEstimata.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru valoarea estimata a exponatului");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru varsta ingrijitor");
                    goto Outer;
                }

                try
                {
                    int nr = Int32.Parse(txtAnProvenienta.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru anul provenientei al exponatului");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru anul provenientei al exponatului");
                    goto Outer;
                }
                try
                {
                    int nr = Int32.Parse(txtGreutateaExponat.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv pentru greutatea exponatului");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru greutatea exponatului");
                    goto Outer;
                }
                try
                {
                    XElement root = new XElement("exponat");

                    root.Add(new XAttribute("id", "Exponat" + getNrExponate()));
                    root.Add(new XElement("codExponat", txtNumeExponat.Text));

                    XElement dataSosire = new XElement("dataSosireMuzeu");
                    DateTime dataSos = this.dataSosireExponat.Value.Date;
                    dataSosire.Add(new XElement("ziSosire", dataSos.Day));
                    dataSosire.Add(new XElement("lunaSosire", dataSos.Month));
                    dataSosire.Add(new XElement("anSosire", dataSos.Year));
                    root.Add(dataSosire);


                    XElement cusca = new XElement("vitrina");
                    cusca.Add(new XElement("nr", txtNrVitrinaExponat.Text));
                    cusca.Add(new XElement("suprafata", txtSuprafataVitrinaExponat.Text));
                    cusca.Add(new XElement("material", txtMaterialVitrina.Text));
                    root.Add(cusca);

                    XElement detalii = new XElement("detalii");
                    detalii.Add(new XElement("anProvenienta", txtAnProvenienta.Text));
                    detalii.Add(new XElement("continent", listaContinente.Text));
                    detalii.Add(new XElement("tipExponat", listTipExponat.Text));
                    detalii.Add(new XElement("varsta", txtVarstaExponat.Text));
                    detalii.Add(new XElement("valoareEstimata", txtValoareEstimata.Text));
                    detalii.Add(new XElement("greutate", txtGreutateaExponat.Text));

                    root.Add(detalii);

                    XElement ingrijitor = new XElement("paznic");
                    ingrijitor.Add(new XElement("nume", txtNumePaznic.Text));
                    ingrijitor.Add(new XElement("prenume", txtPrenumePaznic.Text));
                    ingrijitor.Add(new XElement("varsta", txtVarstaPaznic.Text));
                    root.Add(ingrijitor);

                    doc.Element("historicalmuseum").Element("exponate").Add(root);
                    doc.Save(fileName);
                    MessageBox.Show("Exponat adaugat");
                    curataCampuri();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (listaAdauga.Text == "Vizitator")
            {
                if (txtNumeVizitator.Text == "" || txtPrenumeVizitator.Text == "" || txtVarstaVizitator.Text == "")
                {
                    MessageBox.Show("Completati toate campurile!");
                    goto Outer;
                }
                try
                {
                    int nr = Int32.Parse(txtVarstaVizitator.Text);
                    if (nr <= 0)
                    {
                        MessageBox.Show("Introduceti un nr pozitiv.");
                        goto Outer;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Introduceti un nr pentru varsta");
                    goto Outer;
                }

                try
                {
                    XElement root = new XElement("vizitator");

                    root.Add(new XAttribute("id", "vizitator" + getNrVizitatori()));
                    root.Add(new XElement("nume", txtNumeVizitator.Text));
                    root.Add(new XElement("prenume", txtPrenumeVizitator.Text));
                    root.Add(new XElement("varsta", txtVarstaVizitator.Text));

                    XElement bilet = new XElement("bilet");
                    bilet.Add(new XElement("tipPersoana", tipPersoanaVizitator.Text));
                    bilet.Add(new XElement("pret", pretBiletVizitator.Text));

                    XElement data = new XElement("data");
                    DateTime dataVizita = dateTimePicker1.Value.Date;
                    data.Add(new XElement("zi", dataVizita.Day));
                    data.Add(new XElement("luna", dataVizita.Month));
                    data.Add(new XElement("an", dataVizita.Year));
                    string timp = "" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second;
                    data.Add(new XElement("ora", timp));

                    bilet.Add(data);
                    root.Add(bilet);

                    doc.Element("historicalmuseum").Element("vizitatori").Add(root);
                    doc.Save(fileName);
                    MessageBox.Show("Vizitator adaugat");
                    curataCampuri();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        Outer:;


        }
        public void curataCampuri()
        {
            txtMaterialVitrina.Text = "";
            txtNrVitrinaExponat.Text = "";
            txtNumeExponat.Text = "";
            txtNumePaznic.Text = "";
            txtNumeVizitator.Text = "";
            txtPrenumePaznic.Text = "";
            txtPrenumeVizitator.Text = "";
            txtSuprafataVitrinaExponat.Text = "";
            txtVarstaExponat.Text = "";
            txtVarstaPaznic.Text = "";
            txtVarstaVizitator.Text = "";

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaAdauga.Text == "Exponat")
            {
                groupboxExponat.Visible = true;
                groupBox3.Visible = false;
            }
            else if (listaAdauga.Text == "Ingrijitor")
            {
                groupboxExponat.Visible = false;
                groupBox3.Visible = false;
            }
            else if (listaAdauga.Text == "Vizitator")
            {
                groupboxExponat.Visible = false;
                groupBox3.Visible = true;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void AfisareXML(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            txtTextXML.Text = System.Xml.Linq.XDocument.Parse(doc.InnerXml).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            XmlNodeList nodes = doc.DocumentElement.SelectNodes("/historicalmuseum/reguli/regula");
            List<string> tipReguli = new List<string>();
            foreach (XmlNode nod in nodes)
            {
                tipReguli.Add(nod.SelectSingleNode("tip").InnerText);
            }
            listaReguli.Items.Clear();
            foreach (string regula in tipReguli)
            {
                listaReguli.Items.Add(regula);
            }

        }
        private void Parsare(object sender, EventArgs e)
        {
            if (listaReguli.SelectedItem != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

                XmlNodeList nodes = doc.DocumentElement.SelectNodes("/historicalmuseum/reguli/regula");
                foreach (XmlNode nod in nodes)
                {
                    if (nod.SelectSingleNode("tip").InnerText == listaReguli.SelectedItem.ToString())
                    {
                        //regula 1 //Cauta exponate X  si Y
                        if (listaReguli.SelectedItem.ToString() == "Cauta exponate X si Y")
                        {
                            string Exponat1 = textBox1.Text;
                            string Exponat2 = textBox2.Text;
                            string mesaj = cautaExponateXsiY(Exponat1, Exponat2, nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                        //regula 2
                        else if (listaReguli.SelectedItem.ToString() == "Cauta exponate X sau Y")
                        {
                            string Exponat1 = textBox1.Text;
                            string Exponat2 = textBox2.Text;
                            string mesaj = cautaExponateXsauY(Exponat1, Exponat2, nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                        //regula 3
                        else if (listaReguli.SelectedItem.ToString() == "Cauta exponat X")
                        {
                            string Exponat1 = textBox1.Text;

                            string mesaj = cautaExponatX(Exponat1, nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                        //regula 4
                        else if (listaReguli.SelectedItem.ToString() == "Primire vizitatori?")
                        {
                            string mesaj = primireVizitatori(nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                        //regula 5
                        else if (listaReguli.SelectedItem.ToString() == "Vizitatori cu cel putin X vizite?")
                        {

                            try
                            {
                                int NrVizite = 0;
                                try
                                {
                                    NrVizite = Int32.Parse(textBox1.Text);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Introduceti un numar");
                                }
                                if (NrVizite <= 0)
                                {
                                    MessageBox.Show("Introduceti un numar > 0.");
                                    break;
                                }
                                string mesaj = getVizitatoriCuXVizite(NrVizite, nod);
                                richTextBox1.AppendText(mesaj + "\r\n");
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        else if (listaReguli.SelectedItem.ToString() == "Cauta exponate din continent C")
                        {
                            string continentCautat = comboBox1.Text;

                            string mesaj = ExponateInContinentX(continentCautat, nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                        else if (listaReguli.SelectedItem.ToString() == "Cauta exponate X(tipExponat)")
                        {
                            string tipExponatCautat = comboBox2.Text;

                            string mesaj = ExponateTipExponatX(tipExponatCautat, nod);

                            richTextBox1.AppendText(mesaj + "\r\n");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Alegeti o regula!");
            }

        }
        public string cautaExponateXsiY(string Exponat1, string Exponat2, XmlNode nod)
        {

            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            bool existaExponat1 = false;
            bool existaExponat2 = false;
            string vitrinaExponat1 = "";
            string vitrinaExponat2 = "";
            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");
            foreach (XmlNode Exponat in nodes)
            {
                if (Exponat.SelectSingleNode("codExponat").InnerText == Exponat1)
                {
                    existaExponat1 = true;
                    vitrinaExponat1 = Exponat.SelectSingleNode("/historicalmuseum/exponate/exponat/vitrina/nr").InnerText;
                }
                if (Exponat.SelectSingleNode("codExponat").InnerText == Exponat2)
                {
                    existaExponat2 = true;
                    vitrinaExponat2 = Exponat.SelectSingleNode("/historicalmuseum/exponate/exponat/vitrina/nr").InnerText;
                }
            }
            if (existaExponat1 && existaExponat2)
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%1", Exponat1).Replace("%2", vitrinaExponat1).Replace("%3", Exponat2).Replace("%4", vitrinaExponat2);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }
        }
        public string cautaExponateXsauY(string Exponat1, string Exponat2, XmlNode nod)
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            bool existaExponat1 = false;
            bool existaExponat2 = false;
            string vitrinaExponat1 = "__";
            string vitrinaExponat2 = "__";
            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");
            foreach (XmlNode Exponat in nodes)
            {

                if (Exponat.SelectSingleNode("/historicalmuseum/exponate/exponat/codExponat").InnerText == Exponat1)
                {
                    existaExponat1 = true;
                    vitrinaExponat1 = Exponat.SelectSingleNode("vitrina/nr").InnerText;

                }
                if (Exponat.SelectSingleNode("/historicalmuseum/exponate/exponat/codExponat").InnerText == Exponat2)
                {
                    existaExponat2 = true;
                    vitrinaExponat2 = Exponat.SelectSingleNode("vitrina/nr").InnerText;

                }
            }

            if (existaExponat1 || existaExponat2)
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%1", Exponat1).Replace("%2", vitrinaExponat1).Replace("%3", Exponat2).Replace("%4", vitrinaExponat2);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }
        }
        public string cautaExponatX(string Exponat1, XmlNode nod)
        {

            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            bool existaExponat1 = false;
            string vitrinaExponat1 = "";

            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");
            foreach (XmlNode Exponat in nodes)
            {
                if (Exponat.SelectSingleNode("codExponat").InnerText == Exponat1)
                {
                    existaExponat1 = true;
                    vitrinaExponat1 = vitrinaExponat1 + " si " + Exponat.SelectSingleNode("/historicalmuseum/exponate/exponat/vitrina/nr").InnerText;
                }
            }
            if (existaExponat1)
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%1", Exponat1).Replace("%2", vitrinaExponat1);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }
        }
        public string primireVizitatori(XmlNode nod)
        {

            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/vizitatori/vizitator");
            if (nodes.Count <= Int32.Parse(nod.SelectSingleNode("if/nrMaximVizitatori").InnerText))
            {
                return nod.SelectSingleNode("then").InnerText;
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }

        }
        public string ExponateInContinentX(string continentCautat, XmlNode nod)
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");

            string Exponate = "";
            foreach (XmlNode nodA in nodes)
            {
                if (nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/detalii/continent").InnerText == continentCautat)
                {
                    if (!Exponate.Contains(nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/detalii/tipExponat").InnerText))
                    {
                        Exponate = Exponate + nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/detalii/tipExponat").InnerText + ",";
                    }
                }
            }
            if (Exponate != "")
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%2", Exponate).Replace("%1", continentCautat);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }

        }
        public string ExponateTipExponatX(string tipExponatCautat, XmlNode nod)
        {

            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/exponate/exponat");

            string Exponate = "";
            foreach (XmlNode nodA in nodes)
            {
                if (nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/detalii/tipExponat").InnerText == tipExponatCautat)
                {
                    if (!Exponate.Contains(nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/vitrina/nr").InnerText))
                    {
                        Exponate = Exponate + nodA.SelectSingleNode("/historicalmuseum/exponate/exponat/vitrina/nr").InnerText;
                    }
                }
            }
            if (Exponate != "")
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%2", Exponate).Replace("%1", tipExponatCautat);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText;
            }

        }
        public string getVizitatoriCuXVizite(int nrVizite, XmlNode nod)
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");

            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/vizitatori/vizitator");

            List<Vizitator> vizitatori = new List<Vizitator>();

            foreach (XmlNode nodA in nodes)
            {
                string numeVizNod = nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/nume").InnerText;
                string prenumeVizNod = nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/prenume").InnerText;
                int varstaVizNod = Int32.Parse(nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/varsta").InnerText);
                bool existaInLista = false;
                foreach (Vizitator viz in vizitatori)
                {
                    if (viz.Nume == numeVizNod && viz.Prenume == prenumeVizNod && viz.Varsta == varstaVizNod)
                    {
                        existaInLista = true;
                        break;
                    }

                }
                if (!existaInLista)
                {
                    Vizitator vizitatorNou = new Vizitator();
                    vizitatorNou.Nume = numeVizNod;
                    vizitatorNou.Prenume = prenumeVizNod;
                    vizitatorNou.Varsta = varstaVizNod;
                    vizitatorNou.nrVizite = 0;
                    vizitatori.Add(vizitatorNou);
                }
            }
            //calcul nr vizite
            foreach (Vizitator viz in vizitatori)
            {
                foreach (XmlNode nodA in nodes)
                {
                    string numeVizNod = nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/nume").InnerText;
                    string prenumeVizNod = nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/prenume").InnerText;
                    int varstaVizNod = Int32.Parse(nodA.SelectSingleNode("/historicalmuseum/vizitatori/vizitator/varsta").InnerText);
                    if (viz.Nume == numeVizNod && viz.Prenume == prenumeVizNod && viz.Varsta == varstaVizNod)
                        viz.nrVizite++;
                }
            }

            string listaVizitatoriCuXViz = "";
            foreach (Vizitator viz in vizitatori)
            {
                if (viz.nrVizite >= nrVizite)
                    listaVizitatoriCuXViz = listaVizitatoriCuXViz + viz.Nume + " " + viz.Prenume + ",";
            }
            if (listaVizitatoriCuXViz != "")
            {
                return nod.SelectSingleNode("then").InnerText.Replace("%1", listaVizitatoriCuXViz);
            }
            else
            {
                return nod.SelectSingleNode("else").InnerText.Replace("%1", nrVizite.ToString());
            }
        }
        private void tipPersoanaVizitator_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipPersoanaVizitator.Text == "Adult")
            {
                pretBiletVizitator.Text = "10";
            }
            else
            {
                pretBiletVizitator.Text = "5";
            }
        }
        public int getNrVizitatori()
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");
            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/vizitatori/vizitator");
            return nodes.Count + 1;
        }
        public int getNrExponate()
        {
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\Users\Dobran\Desktop\Tema2-SBC\historicalmuseum.xml");
            XmlNodeList nodes = document.DocumentElement.SelectNodes("/historicalmuseum/Exponate/Exponat");
            return nodes.Count + 1;
        }

        private void listaReguli_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaReguli.SelectedItem.ToString() == "Cauta exponate din continent C")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button5.Enabled = true;
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Africa");
                comboBox1.Items.Add("America de Nord");
                comboBox1.Items.Add("America de Sud");
                comboBox1.Items.Add("Antarctica");
                comboBox1.Items.Add("Asia");
                comboBox1.Items.Add("Australia");
                comboBox1.Items.Add("Europa");

                comboBox1.Text = "Europa";

            }
            else if (listaReguli.SelectedItem.ToString() == "Cauta exponate X(tipExponat)")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox2.Enabled = true;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                comboBox1.Enabled = false;
                button5.Enabled = true;
                comboBox2.Items.Clear();
                comboBox2.Items.Add("Tablou");
                comboBox2.Items.Add("Arma");
                comboBox2.Items.Add("Statuie");
                comboBox2.Items.Add("Coroana");
                comboBox2.Items.Add("Vesminte");
                comboBox2.Items.Add("Armura");
                comboBox2.Items.Add("Bijuterii");
                comboBox2.Items.Add("Vase");

                comboBox2.Text = "Tablou";
            }
            else if (listaReguli.SelectedItem.ToString() == "Vizitatori cu cel putin X vizite?")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                button5.Enabled = true;
            }
            else if (listaReguli.SelectedItem.ToString() == "Primire vizitatori?")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button5.Enabled = true;
            }
            else if (listaReguli.SelectedItem.ToString() == "Cauta Exponat X(tipExponat)")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                button5.Enabled = true;
            }
            else if (listaReguli.SelectedItem.ToString() == "Cauta exponate X sau Y.")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button5.Enabled = true;
            }
            else if (listaReguli.SelectedItem.ToString() == "Cauta exponate X si Y")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                button5.Enabled = true;
            }

            else if (listaReguli.SelectedItem.ToString() == "Cauta exponat X")
            {
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox1.Enabled = true;
                textBox2.Enabled = false;
                button5.Enabled = true;
            }
        }

        //private void txtNumeVizitator_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void label6_Click(object sender, EventArgs e)
        //{

        //}

        //private void label14_Click(object sender, EventArgs e)
        //{

        //}

        //private void txtNumeExponat_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void pictureBox4_Click(object sender, EventArgs e)
        //{

        //}

        //private void label16_Click(object sender, EventArgs e)
        //{

        //}
    }
}
