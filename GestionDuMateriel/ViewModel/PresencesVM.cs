using GestionDuMateriel.Helpers;
using GestionDuMaterielDb.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GestionDuMateriel.ViewModel
{
    public class PresencesVM : BaseViewModel
    {
        private ObservableCollection<PresenceMateriel> _presences;
        private PresenceMateriel _selection;

        private bool _annulationImportEnCours = false;  

        private RondesVM _rondesVM;
        private MaterielsVM _materielsVMDependance;
        private MeublesVM _meublesVMDependance;
        private CodesBarreVM _codesBarreVMDependance;


        // Lors de la ronde en cours, la présence d'un matériel a été constatée sur ou dans le mobilier en cours
        public PresencesVM(RondesVM RondesVM, MaterielsVM MaterielsVMDependance, MeublesVM MeublesVMDependance, CodesBarreVM CodesBarreVMDependance)
        {
            //
            // dépendances ... 
            _rondesVM = RondesVM;
            _materielsVMDependance = MaterielsVMDependance;
            _meublesVMDependance = MeublesVMDependance;
            _codesBarreVMDependance = CodesBarreVMDependance; 
            //
            _presences = new ObservableCollection<PresenceMateriel>(App.Entities().PresenceMateriels);
            // ... autres collections si nécessaire
            if (_presences.Count == 0)
            {
                _selection = null;
            }
            else
            {
                _selection = _presences[0];
            }
        }

        public ObservableCollection<PresenceMateriel> LesPresences
        {
            get { return _presences; }
            set
            {
                _presences.Clear();
                _presences = value;
                FirePropertyChanged("LesPresences");
            }
        }

        public PresenceMateriel LaPresence
        {
            get
            {
                return _selection;
            }
            set
            {
                _selection = value;
                FirePropertyChanged("LaPresence");
            }
        }

        // ponts pour les propriétés des dépendances ...

        public ObservableCollection<Ronde> LesRondes
        {
            get
            {
                return _rondesVM.LesRondes;
            }
            set
            {
                _rondesVM.LesRondes.Clear();
                _rondesVM.LesRondes = value;
                FirePropertyChanged("LesRondes");
            }
        }

        public Ronde LaRonde
        {
            get
            {
                return _rondesVM.LaRonde;
            }
            set
            {
                if (!(_rondesVM.LaRonde == value))
                {
                    if (!(_annulationImportEnCours))
                    {
                        AnnuleImport(); 
                    }
                    _rondesVM.LaRonde = value;
                    FirePropertyChanged("LaRonde");
                }
            }
        }

        public ObservableCollection<Materiel> LesMateriels
        {
            get
            {
                return _materielsVMDependance.LesMateriels;
            }
            set
            {
                _materielsVMDependance.LesMateriels.Clear();
                _materielsVMDependance.LesMateriels = value;
                FirePropertyChanged("LesMateriels");
            }
        }

        public Materiel LeMateriel
        {
            get
            {
                return _materielsVMDependance.LeMateriel;
            }
            set
            {
                _materielsVMDependance.LeMateriel = value;
                FirePropertyChanged("LeMateriel");
            }
        }

        public ObservableCollection<Meuble> LesMeubles
        {
            get
            {
                return _meublesVMDependance.LesMeubles;
            }
            set
            {
                _meublesVMDependance.LesMeubles.Clear();
                _meublesVMDependance.LesMeubles = value;
                FirePropertyChanged("LesMeubles");
            }
        }

        public Meuble LeMeuble
        {
            get
            {
                return _meublesVMDependance.LeMeuble;
            }
            set
            {
                _meublesVMDependance.LeMeuble = value;
                FirePropertyChanged("LeMeuble");
            }
        }

        public void AjoutNouveauCodeBarre(bool bIgnore, string sInterpretation, string sCodeBarre, Ronde ronde)
        {
            if (ronde == LaRonde)
            {
                _codesBarreVMDependance.Ajout();
                _codesBarreVMDependance.LeCodeBarre.Ignore = bIgnore;
                _codesBarreVMDependance.LeCodeBarre.Interpretation = sInterpretation;
                _codesBarreVMDependance.LeCodeBarre.NoDeSerie = sCodeBarre;
            }
            else
            {
                throw new Exception("Erreur dans AjoutNouveauCodeBarre. La ronde voulue n'est pas sélectionnée. "); 
            }
        }

        public void AjoutPresence(Ronde LaRonde, Meuble LeMobilier, Materiel LeMaterielPresent)
        {
            PresenceMateriel nouvellePresence = new PresenceMateriel();
            nouvellePresence.Materiel = LeMaterielPresent;
            nouvellePresence.Meuble = LeMobilier;
            nouvellePresence.Ronde = LaRonde;
            //
            // nouvellePresence.Id  // Entity gère ceci ! 
            //
            App.Entities().PresenceMateriels.Add(nouvellePresence);
            App.Entities().SaveChanges();
            // applique la suppression aux objets représentant les données ... 
            LesPresences.Add(nouvellePresence);
            _selection = nouvellePresence; // met à jour la sélection 
            FirePropertyChanged("LesPresences");
            FirePropertyChanged("LaPresence");
        }

        public ICommand Enregistre { get { return new ExecutableCommand(Enregistrement); } }
        public void Enregistrement()
        {
            App.Entities().SaveChanges();
        }

        public ICommand NouvelleImportation { get { return new ExecutableCommand(DemarreImport); } }
        public void DemarreImport()
        {
            AjoutNouvelleRonde();


            FirePropertyChanged("AnnulerImportVisibility");
        }

        public ICommand AnnulationImportation { get { return new ExecutableCommand(AnnuleImport); } }
        public void AnnuleImport()
        {
            if (!(LaRonde == null))
            {
                if (!(LaRonde.ImportationFaite))
                {
                    _annulationImportEnCours = true;
                    SuppressionRondeSelectionnee();
                    _annulationImportEnCours = false;
                    FirePropertyChanged("AnnulerImportVisibility");
                }
            }
        }

        // Ajout -> AjouteNouvelleRonde
        public ICommand AjouteNouvelleRonde { get { return new ExecutableCommand(AjoutNouvelleRonde); } }
        public void AjoutNouvelleRonde()
        {
            _rondesVM.AjoutNouvelleRonde();
        }

        // Supprime -> SupprimeRondeSelectionnee
        public ICommand SupprimeRondeSelectionnee { get { return new ExecutableCommand(SuppressionRondeSelectionnee); } }
        public void SuppressionRondeSelectionnee()
        {
            _rondesVM.SuppressionSelection(); 
        }

        public Visibility AnnulerImportVisibility
        {
            get
            {
                Visibility result;
                if (LaRonde == null)
                {
                    result = Visibility.Collapsed;
                }
                else
                {
                    if (LaRonde.ImportationFaite)
                    {
                        result = Visibility.Collapsed;
                    }
                    else
                    {
                        result = Visibility.Visible;
                    }
                }
                return result;
            }
        }

        // la fonctionnalité de l'application ==>> l'importation ...
        public ICommand DemarreProcedureImport { get { return new ExecutableCommand(ProcedureImportation); } }
        public void ProcedureImportation()
        {
            const int const_vide = -1;
            if (File.Exists(LaRonde.ImportationCheminCompletFichier))
            {
                LaRonde.ImportationDate = DateTime.Now; // importation faite = true  :-) 
                FirePropertyChanged("LaRonde");
                StreamReader sr = new StreamReader(LaRonde.ImportationCheminCompletFichier);
                string contenuLigne, codeBarre, interpretation, emplacement;
                // LaRonde est déjà connue ...
                Meuble meuble = null;
                Materiel materiel = null; 
                int len;
                bool ignore;
                emplacement = "emplacement où vous avez scanné ! Mobilier à inventorier ? "; 
                while (!(sr.EndOfStream))
                {
                    contenuLigne = sr.ReadLine();
                    interpretation = "pas interprété !";
                    ignore = true; 
                    len = const_vide;
                    len = contenuLigne.IndexOf(' ');
                    if (len == const_vide) len = contenuLigne.Length;
                    codeBarre = contenuLigne.Substring(0, len);
                    if (string.IsNullOrEmpty(codeBarre))
                    {
                        codeBarre = "(vide)";
                    }
                    // check si meuble ...
                    _meublesVMDependance.RechercheAvecBarreCode(codeBarre);
                    if (!(_meublesVMDependance.ResultatDeRechercheAvecBarreCode() == null))
                    {
                        meuble = _meublesVMDependance.ResultatDeRechercheAvecBarreCode();
                        emplacement = meuble.Description.Trim() + ", pièce " + meuble.Piece.Description.Trim() +
                            " (" + meuble.Piece.Plan.Description.Trim() + (meuble.Employe == null ? "" : " / " +
                            meuble.Employe.Description.Trim()) + ")";
                        ignore = false;
                        interpretation = "Scan des objets dans/sur meuble " + emplacement;
                    }
                    else
                    {
                        _materielsVMDependance.RechercheAvecBarreCode(codeBarre);
                        if (!(_materielsVMDependance.ResultatDeRechercheAvecBarreCode() == null))
                        {
                            materiel = _materielsVMDependance.ResultatDeRechercheAvecBarreCode();
                            if (!(meuble == null))
                            {
                                ignore = false;
                                interpretation = "Materiel " + materiel.Description.Trim() +
                                    " vu le " + LaRonde.Date.ToString("dddd d MMMM yyyy") + " (" + emplacement + ")";
                                AjoutPresence(LaRonde, meuble, materiel);
                                materiel.DateDernierApercu = LaRonde.Date;
                                materiel.CommentaireDernierApercu = interpretation;
                            }
                        }
                        else
                        {
                            interpretation = "Code-barre non-reconnu ! Matériel à inventorier ? (" + emplacement + ")";
                        }
                    }
                    AjoutNouveauCodeBarre(ignore, interpretation, codeBarre, LaRonde);
                    //MessageBox.Show(interpretation, ignore ? "ignore" : "pris en compte");
                }
            }
            else
            {
                MessageBox.Show("Le chemin du fichier n'est pas valide. Annulation de l'importation ! ", "Importation des codes-barre", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        






    }

}
