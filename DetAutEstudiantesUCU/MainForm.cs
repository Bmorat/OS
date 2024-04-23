using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;

namespace MultiFaceRec
{
    public partial class FrmPrincipal : Form
    {
        //Declaracion de todas las variables
        Image<Bgr, Byte> currentFrame;
        Capture grabber;
        HaarCascade face;
        HaarCascade eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels= new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;

        private void groupBox2_Enter(object sender, EventArgs e)
        {
        }

        public FrmPrincipal()
        {
            InitializeComponent();
            //Carga los rostros ya cargados
            face = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                //Carga de rsotros entrenadas previamente para cada imagen.
                string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
                string[] Labels = Labelsinfo.Split('%');
                NumLabels = Convert.ToInt16(Labels[0]);
                ContTrain = NumLabels;
                string LoadFaces;
                for (int tf = 1; tf < NumLabels+1; tf++)
                {
                    LoadFaces = "face" + tf + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                    labels.Add(Labels[tf]);
                }            
            }
            catch(Exception e)
            {
                MessageBox.Show("Agrega un rostro.", "Carga de Rostros", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Inicializa el dispositivo de captura
            grabber = new Capture();
            grabber.QueryFrame();
            //Inicializa el evento FrameGraber
            Application.Idle += new EventHandler(FrameGrabber);
            button1.Enabled = false;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Por favor ingresa el nombre para agregar el Estudiante");
                }
                else
                {
                    //Contador de rostros
                    ContTrain = ContTrain + 1;

                    //Obtener un marco gris del dispositivo de captura
                    gray = grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                    //Detector de rostros
                    MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2,10,Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,new Size(20, 20));

                    //Acción para cada elemento detectado
                    foreach (MCvAvgComp f in facesDetected[0])
                    {
                        TrainedFace = currentFrame.Copy(f.rect).Convert<Gray, byte>();
                        break;
                    }
                    //Cambiar el tamaño de la imagen de la cara detectada para forzar la comparación del mismo tamaño con la imagen de prueba con método de tipo.
                    TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                    trainingImages.Add(TrainedFace);
                    labels.Add(textBox1.Text);

                    //Mostrar cara agregada en escala de grises
                    imageBox1.Image = TrainedFace;
                    File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.ToArray().Length.ToString() + "%");

                    //Etiquetas de los rostros seleccionadss en un archivo de texto para cargarlos
                    for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
                    {
                        trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels.ToArray()[i - 1] + "%");
                    }
                    MessageBox.Show(textBox1.Text + ", rostro detectado y agregado", "Training OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("No se detectó ningún rostro. Por favor acérquese.", "Training Fail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            NamePersons.Add("");

            //Obtener el dispositivo de captura de formato de cuadro actual
            currentFrame = grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convertirlo en escala de grises
            gray = currentFrame.Convert<Gray, Byte>();

            //Detector de rostros
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));

            //Accion para cada elemento detectado
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = currentFrame.Copy(f.rect).Convert<Gray, byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //dibuja la cara detectada con color verde
                currentFrame.Draw(f.rect, new Bgr(Color.Green), 2);

                if (trainingImages.ToArray().Length != 0)
                {
                    //Reconocimiento facial con una cantidad de imágenes entrenadas
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(),labels.ToArray(),3000,ref termCrit);name = recognizer.Recognize(result);

                    //Dibuja la etiqueta para cada rostro detectado y reconocido
                    currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));
                }
                NamePersons[t - 1] = name;
                NamePersons.Add("");

                //Muestra el número de rostros detectados en el label3
                label3.Text = facesDetected[0].Length.ToString();
            }
            t = 0;

            //Concatenación de nombres de las personas reconocidas
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
            }
            //Muestra del los rostros reconocidos.
            imageBoxFrameGrabber.Image = currentFrame;
            label4.Text = names;
            names = "";
            //Limpia la celda de la lista de nombres
            NamePersons.Clear();
        }
    }
}
