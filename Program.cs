using System;
using System.ComponentModel.Design.Serialization;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Office {
    public class Workers {
        private string _name;
        private int _warea;

        public string name { get { return _name; } set { _name = value; } }
        public int warea { get { return _warea; } set { _warea = value; } }

        public Workers(string name, int warea) {
            this._name = name;
            this._warea = warea;
        }
    }

    class Program {
        static void Main(string[] args) {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<Workers> workers = new List<Workers>();
            while (true) {
                Console.Clear();
                Console.WriteLine("-h para lista de comandos\n\n");

                string choose = Console.ReadLine();

                if (choose[0] == '-') {
                    switch (choose[1]) { // ! [0] e [1] pq supostamente é pra ser só dois caracteres: '-' e 'a', 'l', 'e', etc
                        case 'a':
                            Console.Write("\nNome: ");
                            string new_name = Console.ReadLine();

                            Console.Write("Setor 1 -> Escritório\nSetor 2 -> Fábrica\n\nSetor: ");
                            int new_warea;
                            while (!int.TryParse(Console.ReadLine(), out new_warea) || (new_warea <= 0)) {
                                Console.Write("Entrada inválida...\nQual o setor em que ele trabalha? ");
                            }

                            workers.Add(new Workers(new_name, new_warea));

                            Console.Write($"\n{new_name} registrado com sucesso!\nAperte ENTER para continuar...");
                            Console.ReadKey();
                        break;
                        case 'e':
                            // TODO: editar alguém
                            Console.Write($"\nAinda não implementado...\nAperte ENTER para continuar...");
                            Console.ReadKey();
                        break;
                        case 'r':
                            // TODO: remover alguém
                            Console.Write("Ainda não implementado...\nAperte ENTER para continuar...");
                            Console.ReadKey();
                        break;
                        case 'l':
                            if (workers.Count == 0) {
                                Console.WriteLine("Nenhum trabalhador registrado! Use '-a'");
                            } else {
                                foreach (Workers worker in workers) {
                                    Console.Write($"• {worker.name}, do setor {worker.warea}\n");
                                }
                            }
                            Console.Write($"\nAperte ENTER para continuar...");
                            Console.ReadKey();
                        break;
                        case 's':
                        Console.Write("Deseja salvar o arquivo em um nome específico (S) ou padrão (N)? S / N\n");
                            string input = Console.ReadLine();
                            char opt = input.ToUpper()[0];

                            if (opt == 'N') {
                                saveToFile("workers_list.txt", workers);
                            } else if (opt == 'S') {
                                Console.WriteLine("Qual o nome do arquivo?");
                                string path = Console.ReadLine();
                                if (!path.EndsWith(".txt"))
                                    path += ".txt";
                                saveToFile(path, workers);
                            } else {
                                Console.WriteLine("Opção inválida. Usando nome padrão.");
                                saveToFile("workers_list.txt", workers);
                            }
                        break;
                        case 'q':
                            Console.Write("Deseja salvar em um arquivo de texto (recomendado)? S / N\n");
                            input = Console.ReadLine();
                            opt = input.ToUpper()[0];

                            switch (opt) {
                                case 'S':
                                    saveToFile("workers_list.txt", workers);
                                break;

                                default:
                                    saveToFile("workers_list.txt", workers);
                                break;

                                case 'N':
                                    return;
                            }

                            Console.Write("Saindo...");
                            return;
                        case 'h':
                            Console.WriteLine("-a -> Registrar trabalhador.");
                            Console.WriteLine("-e -> Editar trabalhador.");
                            Console.WriteLine("-r -> Remover trabalhador.");
                            Console.WriteLine("-l -> Ver a lista de trabalhadores.");
                            Console.WriteLine("-h -> Ver a lista de comandos.");
                            Console.WriteLine("-s -> Salvar lista de trabalhadores.");
                            Console.WriteLine("-q -> Fechar programa.");
                            Console.WriteLine("Aperte ENTER para continuar.");
                            Console.ReadKey();
                        break;

                        default:
                            Console.Write("Use os comandos mostrados.");
                            Console.Write($"\nAperte ENTER para continuar...");
                            Console.ReadKey();
                        break;
                    }
                }
            }
        }

        public static void saveToFile(string path, List<Workers> workers) {
            string content = "";
            foreach (Workers worker in workers) {
                content+=$"• {worker.name}, {worker.warea} anos\n";
            }
            File.WriteAllText(path, content, Encoding.UTF8);
        }
    }
}
