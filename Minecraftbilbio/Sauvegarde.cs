using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Minecraftbilbio
{
    public class Sauvegarde
    {
        public static int bordureXMin = -512;
        public static int bordureXMax = 512;
        public static int bordureYMin = -512;
        public static int bordureYMax = 512;
        public static bool piece = true;

        public static byte zoom_Block = 2;
        public static byte zoom_HotBarre = 2;

        public static Monde monde;
        public static Joueur joueur;

        /// <summary>
        /// Permet de Sauvegarder un monde à l'endroit que tu veux</br>
        /// System.IO.Directory.GetCurrentDirectory() pour recuperer le dossier de l'application
        /// </summary>
        /// <param name="monde"></param>
        /// <param name="path"></param>
        public static void SauvegarderMonde(Monde monde, string path)
        {
            if (Directory.Exists(path)) { Directory.Delete(path, true); }

            Directory.CreateDirectory(path);
            File.WriteAllLines(path + "\\info.txt", new string[] { "Generateur:" + monde.Generateur.Nom, "Seed:" + monde.Generateur.Noise.Seed });

            Directory.CreateDirectory(path + "\\Entities");
            for (int i = 0; i < monde.Entites.Count; i++)
            {
                Entite ent = monde.Entites[i];
                Directory.CreateDirectory(path + "\\Entities\\" + i + "." + ent.id());
                ent.Sauvegarder(path + "\\Entities\\" + i + "." + ent.id());
            }

            foreach (KeyValuePair<string, Chunk> chunks in monde.Chunks)
            {
                string[] s = chunks.Key.Split('/');
                string c = s[0] + "." + s[1] + "." + chunks.Value.Generer;

                Directory.CreateDirectory(path + "\\Chunks\\" + c);
                for (int bx = 0; bx < 16; bx++)
                    for (int by = 0; by < 16; by++)
                    {
                        Block block = chunks.Value.Blocks[bx, by];
                        if (block != null)
                        {
                            Directory.CreateDirectory(path + "\\Chunks\\" + c + "\\" + bx + "." + by + "." + block.Name);
                            block.Sauvegarder(path + "\\Chunks\\" + c + "\\" + bx + "." + by + "." + block.Name);
                        }
                    }
            }
        }

        /// <summary>
        /// Permet de Charger un monde</br>
        /// System.IO.Directory.GetCurrentDirectory() pour recuperer le dossier de l'application</br>
        /// le monde recuperer (si le dossier du monde exists) il sera dans Sauvegarde.monde</br>
        /// le joueur sera initer avec le monde
        /// </summary>
        /// <param name="path"></param>
        public static void ChargerMonde(string path)
        {
            if (Directory.Exists(path))
            {
                string[] mondeOptions = File.ReadAllLines(path + "\\info.txt");
                long seed = long.Parse(mondeOptions[1].Split(':')[1]);
                Generateur generateur = null;
                switch (mondeOptions[0].Split(':')[1])
                {
                    case "GenerateurParDefault":
                        generateur = new GenerateurParDefault(new Noise(seed));
                        break;
                    case "VoidGenerateur":
                        generateur = new VoidGenerateur(new Noise(seed));
                        break;
                    case "FlatGenerateur":
                        generateur = new FlatGenerateur(new Noise(seed));
                        break;
                }
                if (generateur != null)
                {
                    string[] ents = Directory.GetDirectories(path + "\\Entities");
                    int max = -1;
                    List<KeyValuePair<int, Entite>> entities = new List<KeyValuePair<int, Entite>>();
                    foreach (string entpath in ents)
                    {
                        string[] n = entpath.Split('\\');
                        string[] a = n[n.Length - 1].Split('.');
                        Entite ent = Entite.Entites()[a[1]].Charger(entpath);
                        int i = int.Parse(a[0]);
                        if (i > max)
                        {
                            max = i;
                        }
                        entities.Add(new KeyValuePair<int, Entite>(i, ent));
                        if (ent is Joueur j)
                        {
                            joueur = j;
                        }
                    }

                    Entite[] entites = new Entite[max + 1];
                    foreach (KeyValuePair<int, Entite> ent in entities)
                    {
                        entites[ent.Key] = ent.Value;
                    }

                    monde = new Monde(generateur, entites.ToList());

                    string[] chunks = Directory.GetDirectories(path + "\\Chunks");
                    foreach (string cpath in chunks)
                    {
                        string[] blocksFile = Directory.GetDirectories(cpath);

                        Block[,] blocks = new Block[16, 16];

                        foreach (string blockfile in blocksFile)
                        {
                            string[] bnom = blockfile.Split('\\');
                            string[] bcoord = bnom[bnom.Length - 1].Split('.');
                            int bx = int.Parse(bcoord[0]);
                            int by = int.Parse(bcoord[1]);
                            Block block = Block.Blocks()[bcoord[2]].Charger(blockfile);
                            blocks[bx, by] = block;
                        }

                        string[] cnom = cpath.Split('\\');
                        string[] coord = cnom[cnom.Length - 1].Split('.');
                        int cx = int.Parse(coord[0]);
                        int cy = int.Parse(coord[1]);
                        bool generer = bool.Parse(coord[2]);
                        monde.SetChunk(cx, cy, new Chunk(blocks, generer));
                    }
                }
            }
        }

        /// <summary>
        /// sauvegarde les options de l'utilisateur (les parametres static de cette classe)</br>
        /// System.IO.Directory.GetCurrentDirectory() pour recuperer le dossier de l'application
        /// </summary>
        /// <param name="path"></param>
        public static void SauvegarderOption(string path)
        {
            if (File.Exists(path)) { File.Delete(path); }
            File.WriteAllLines(path, new string[] {
                "bordureXMin:"+bordureXMin.ToString(),
                "bordureXMax:"+bordureXMax.ToString(),
                "bordureYMin:"+bordureYMin.ToString(),
                "bordureYMax:"+bordureYMax.ToString(),
                "piece:"+piece.ToString(),
                "zoom_Block:"+zoom_Block.ToString(),
                "zoom_HotBarre:"+zoom_HotBarre.ToString()});
        }

        /// <summary>
        /// Charge les options de l'utilisateur (les parametres static de cette classe)</br>
        /// System.IO.Directory.GetCurrentDirectory() pour recuperer le dossier de l'application
        /// </summary>
        /// <param name="path"></param>
        public static void ChargerOption(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    string[] s = File.ReadAllLines(path);
                    bordureXMin = int.Parse(s[0].Split(':')[1]);
                    bordureXMax = int.Parse(s[1].Split(':')[1]);
                    bordureYMin = int.Parse(s[2].Split(':')[1]);
                    bordureYMax = int.Parse(s[3].Split(':')[1]);
                    piece = bool.Parse(s[4].Split(':')[1]);
                    zoom_Block = byte.Parse(s[5].Split(':')[1]);
                    zoom_HotBarre = byte.Parse(s[6].Split(':')[1]);
                }
                catch (Exception) { }
            }
        }
    }
}
