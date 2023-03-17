using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd2.Model;

namespace BackEnd2.Database
{
    public class MyDBContext
    {
    //    public DbSet<FicheTechnique> FicheTec { get; set; }

    //    public DbSet<Produit> Product { get; set; }

    //    public DbSet<Machine> mMachine { get; set; }

    //    public DbSet<chaine> Chaine { get; set; }

    //    public DbSet<ChaineMatrix> ChaineMatrix { get; set; }


    //    public DbSet<Catalogue> Catalogue { get; set; }

    //    public DbSet<Titrage> Titrage { get; set; }

    //    public DbSet<Composant> Composant { get; set; }

    //    public DbSet<Composition> Composition { get; set; }

    //    public DbSet<Couleur> Color { get; set; }

    //    public DbSet<Duitages> Duitages { get; set; }

    //    public DbSet<DuitageGomme> DuitageGomme { get; set; }

    //    public DbSet<Verificateur> Verificateur { get; set; }

    //    public DbSet<Concepteur> Concepteur { get; set; }
    //    public DbSet<Enfilage> Enfilage { get; set; }

    //    public DbSet<EnfilageMatrix> EnfilageMatrix { get; set; }

    //    public DbSet<Matiere> Matiere { get; set; }

    //    public DbSet<ModelMachine> ModelMachine { get; set; }

    //    public DbSet<Client> Client { get; set; }

    //    public DbSet<Produit> Produit { get; set; }

    //    public DbSet<TypeMatiere> TypeMatiere { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        var ConString = new SqliteConnectionStringBuilder
    //        {
    //            DataSource = "FicheTeK.db"
    //            //Password = "TTBM1971"
    //        };
    //        var sqlite = new SqliteConnection(ConString.ToString());


    //        optionsBuilder.UseSqlite(sqlite);
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<chaine>().ToTable(nameof(Chaine));


    //        modelBuilder.Entity<Composant>().ToTable(nameof(Composant));
    //        modelBuilder.Entity<Couleur>().ToTable(nameof(Color));
    //        modelBuilder.Entity<Duitages>().ToTable(nameof(Duitages));
    //        modelBuilder.Entity<Titrage>().ToTable(nameof(Titrage));

    //        modelBuilder.Entity<Enfilage>().ToTable(nameof(Enfilage));
    //        modelBuilder.Entity<EnfilageMatrix>().ToTable(nameof(EnfilageMatrix));
    //        modelBuilder.Entity<FicheTechnique>().ToTable(nameof(FicheTechnique));
    //        modelBuilder.Entity<Machine>().ToTable(nameof(mMachine));
    //        modelBuilder.Entity<Matiere>().ToTable(nameof(Matiere));
    //        modelBuilder.Entity<ModelMachine>().ToTable(nameof(ModelMachine));
    //        modelBuilder.Entity<Produit>().ToTable(nameof(Product)).HasOne(pr => pr.PeigneObj)
    //            .WithMany(pn => pn.Products).HasForeignKey(pr => pr.Peigne);
    //        modelBuilder.Entity<Produit>().ToTable(nameof(Product)).HasOne(pr => pr.RedacteurObj)
    //            .WithMany(red => red.Products).HasForeignKey(pr => pr.Redacteur);
    //        modelBuilder.Entity<TypeMatiere>().ToTable(nameof(TypeMatiere));
    //        modelBuilder.Entity<Duitages>().HasOne(duit => duit.Machine).WithMany(m => m.GetDuitages)
    //            .OnDelete(DeleteBehavior.Cascade);
    //        modelBuilder.Entity<EnfilageMatrix>().HasKey(table => new { table.ID, table.x, table.y });
    //        modelBuilder.Entity<Produit>().HasKey(table => new { table.Id });
    //        modelBuilder.Entity<EnfilageMatrix>().HasOne(enm => enm.Enf).WithMany(en => en.GetMatrix)
    //            .HasForeignKey(enm => enm.EnfID).OnDelete(DeleteBehavior.Cascade);


    //        modelBuilder.Entity<Produit>().HasMany(pr => pr.GetComposition).WithOne(comp => comp.ProdID)
    //            .OnDelete(DeleteBehavior.Cascade);
    //        modelBuilder.Entity<Produit>().Ignore(p => p.FicheTekID);
    //        modelBuilder.Entity<Produit>().HasOne<FicheTechnique>().WithMany(f => f.Produits)
    //            .HasForeignKey(p => p.FicheId).OnDelete(DeleteBehavior.Cascade);
    //        //modelBuilder.Entity<Produit>().HasOne(p => p.DuitageID);
    //    }

    //    public List<FicheTechnique> GetFicheTechniques()
    //    {
    //        return FicheTec.Include(ft => ft.Catalog)
    //            .Include(ft => ft.Produits)
    //            .ThenInclude(pr => pr.Concepteur)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.Client)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.Verificateur)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.GetComposition).ThenInclude(comp => comp.GetMatiere)
    //            .ThenInclude(ma => ma.Titrage).ThenInclude(tit => tit.TypeMatiere)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.GetComposition).ThenInclude(comp => comp.GetMatiere)
    //            .ThenInclude(ma => ma.GetCouleur)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.GetComposition)
    //            .ThenInclude(compo => compo.GetComposant)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.EnfilageID).ThenInclude(enf => enf.GetMatrix)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.EnfilageID).ThenInclude(enf => enf.GetChaine)
    //            .ThenInclude(ch => ch.ChMatrix)
    //            .Include(ft => ft.Produits).ThenInclude(pr => pr.DuitageID).AsNoTracking().ToList();
    //    }

    //    public List<Matiere> GetMatieres()
    //    {
    //        return Matiere.ToList();
    //    }

    //    public List<chaine> GetChaines()
    //    {
    //        return Chaine.Include(ch => ch.ChMatrix).ToList();
    //    }

    //    public List<Duitages> GetDuitageMachine(Machine mm)
    //    {
    //        return Duitages.ToList().FindAll(duit => duit.Machine.ID == mm.ID);
    //    }


    //    public List<DuitageGomme> GetDuitageGoMachine(Machine mm)
    //    {
    //        return DuitageGomme.ToList().FindAll(duit => duit.Machine.ID == mm.ID);
    //    }

    //    public Titrage GetTitrage(Titrage tit)
    //    {
    //        return Titrage.ToList().Find(tits => tits.ID == tit.ID);
    //    }

    //    public TypeMatiere GetTypeMatiereNom(string nom)
    //    {
    //        return TypeMatiere.ToList().Find(typmat => typmat.MatiereNom.ToLower().Equals(nom.ToLower()));
    //    }

    //    public List<Titrage> GetTitrageByTypMat(TypeMatiere tm)
    //    {
    //        return Titrage.ToList().FindAll(t => t.TypeMatiere.ID == tm.ID);
    //    }

    //    public List<TypeMatiere> GetTypeMatieres()
    //    {
    //        return TypeMatiere.ToList();
    //    }

    //    public List<Machine> GetMachines()
    //    {
    //        return mMachine.ToList();
    //    }

    //    public List<Machine> GetCrochtageMachines()
    //    {
    //        return mMachine.Where(m => m.DoubleDuitage == 1).ToList();
    //    }

    //    public List<Composant> GetComposants()
    //    {
    //        return Composant.ToList();
    //    }

    //    public List<Composition> GetCompositions(int ProdId, int ProdVersion)
    //    {
    //        return Composition.Where(compo => compo.ProdID.Id == ProdId && compo.ProdID.Version == ProdVersion)
    //            .ToList();
    //    }

    //    public List<Composition> GetCompositions()
    //    {
    //        return Composition.ToList();
    //    }

    //    public async Task<List<Couleur>> GetCouleurs()
    //    {
    //        return await Task.Run(() =>
    //            Color.ToList()
    //        );
    //    }

    //    public List<Client> GetClients()
    //    {
    //        return Client.ToList();
    //    }

    //    public List<Concepteur> GetConcepteur()
    //    {
    //        return Concepteur.ToList();
    //    }

    //    public List<Verificateur> GetVerificateur()
    //    {
    //        return Verificateur.ToList();
    //    }

    //    public int GetLastProductID()
    //    {
    //        if (Produit == null || Produit.Count() == 0)
    //            return 0;
    //        return Produit.ToList().OrderBy(p => p.Id).LastOrDefault().Id;
    //    }

    //    public bool CheckNArticleUnique(int NumArticle, int ProdID)
    //    {
    //        var prod = Produit.ToList().Find(pr => pr.NumArticle == NumArticle && pr.Id != ProdID);
    //        if (prod != null)
    //            return false;
    //        return true;
    //    }

    //    public bool CheckVersionUnique(Produit NewProd)
    //    {
    //        var prod = Produit.ToList().Find(pr => (pr.Id == NewProd.Id) & (pr.Version == NewProd.Version));
    //        if (prod != null)
    //            return false;
    //        return true;
    //    }

    //    public int AddNewProduct(Produit NewProd)
    //    {
    //        Produit.Add(NewProd);
    //        SaveChanges();
    //        return NewProd.Id;
    //    }

    //    public void UpdateProdNumArticle(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.NumArticle = NewProd.NumArticle;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdRef(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Ref = NewProd.Ref;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdVersion(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Version = NewProd.Version;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdName(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Name = NewProd.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdName2(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Name2 = NewProd.Name2;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdClient(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Client = NewProd.Client;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdLargeur(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Largeur = NewProd.Largeur;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdEpaiseur(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Epaisseur = NewProd.Epaisseur;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdDent(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Dent = NewProd.Dent;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdPeigne(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Peigne = NewProd.Peigne;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdDuitage(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.DuitageID = NewProd.DuitageID;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdDuitageGomme(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.DuitageGomme = NewProd.DuitageGomme;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdConcepteur(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Concepteur = NewProd.Concepteur;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdVerificateur(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Verificateur = NewProd.Verificateur;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdRedaction(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.Redaction = NewProd.Redaction;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdDateCreation(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.DateCreation = NewProd.DateCreation;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdMiseAJour(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.MiseAJour = NewProd.MiseAJour;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateProdIsEnfilage(Produit NewProd)
    //    {
    //        var res = Produit.SingleOrDefault(pr => pr.Id == NewProd.Id);
    //        if (res != null)
    //        {
    //            res.IsEnfilage = NewProd.IsEnfilage;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateFicheTechniqueCategorie(FicheTechnique Ft)
    //    {
    //        var res = FicheTec.SingleOrDefault(ft => ft.ID == Ft.ID);
    //        if (res != null)
    //        {
    //            res.Catalog = Ft.Catalog;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateFicheTechniqueCategorie(int FtID, Catalogue cat)
    //    {
    //        var res = FicheTec.SingleOrDefault(ft => ft.ID == FtID);
    //        if (res != null)
    //        {
    //            res.Catalog = cat;
    //            SaveChanges();
    //        }
    //    }

    //    public Composition AddProdCompo(Composition compo)
    //    {
    //        Entry(compo).State = EntityState.Added;
    //        if (compo.ProdID != null)
    //        {
    //            var prod = Produit.SingleOrDefault(pr => pr.Id == compo.ProdID.Id);
    //            compo.ProdID = prod;
    //        }

    //        Composition.Add(compo);
    //        SaveChanges();
    //        return compo;
    //    }

    //    public void RemoveProdCompo(Composition compo)
    //    {
    //        var delComp = Composition.SingleOrDefault(cmp => cmp.ID == compo.ID);
    //        if (delComp != null)
    //        {
    //            Composition.Remove(delComp);
    //            SaveChanges();
    //        }
    //    }

    //    public void AddProdEnfilage(Enfilage enfilage)
    //    {
    //        Enfilage.Add(enfilage);
    //        SaveChanges();
    //    }

    //    public void AddEnfilageElement(EnfilageMatrix EnfilageElement)
    //    {
    //        EnfilageMatrix.Add(EnfilageElement);
    //        SaveChanges();
    //    }

    //    public void UpdateEnfilageElement(EnfilageMatrix EnfilageElement)
    //    {
    //        var res = EnfilageMatrix.SingleOrDefault(enfEl => enfEl.ID == EnfilageElement.ID);
    //        if (res != null)
    //        {
    //            res.value = EnfilageElement.value;
    //            SaveChanges();
    //        }
    //    }

    //    public void RemoveEnfilage(Enfilage mEnfilage)
    //    {
    //        var DelEnf = Enfilage.SingleOrDefault(enf => enf.ID == mEnfilage.ID);
    //        if (DelEnf != null)
    //        {
    //            Enfilage.Remove(mEnfilage);
    //            SaveChanges();
    //        }
    //    }

    //    public void RemoveEnfilageElement(EnfilageMatrix EnfilageElement)
    //    {
    //        var enfmat = EnfilageMatrix.SingleOrDefault(enfmx => enfmx.ID == EnfilageElement.ID);
    //        if (enfmat != null)
    //        {
    //            EnfilageMatrix.Remove(EnfilageElement);
    //            SaveChanges();
    //        }
    //    }

    //    public void AddChaineElement(ChaineMatrix ChaineElement)
    //    {
    //        ChaineMatrix.Add(ChaineElement);
    //        SaveChanges();
    //    }

    //    public void RemoveChaineElement(ChaineMatrix ChaineElement)
    //    {
    //        var chmat = ChaineMatrix.SingleOrDefault(chmatx => chmatx.ID == ChaineElement.ID);
    //        if (chmat != null)
    //        {
    //            ChaineMatrix.Remove(ChaineElement);
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoComposant(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);

    //        var res2 = Composant.SingleOrDefault(comps => comps.ID == compo.GetComposant.ID);

    //        var nikmok = compo == res;
    //        if (res != null)
    //        {
    //            res.GetComposant = res2;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoNumComposant(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.NumComposant = compo.NumComposant;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoNum(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Num = compo.Num;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoNbrFil(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.NbrFil = compo.NbrFil;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoMatiere(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.GetMatiere = compo.GetMatiere;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoTorsion(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Torsion = compo.Torsion;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoEnfilage(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Enfilage = compo.Enfilage;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoEmb(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Emb = compo.Emb;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoPoids(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Poids = compo.Poids;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoEnfNbr(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.EnfNbrFil = compo.EnfNbrFil;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateCompoObservation(Composition compo)
    //    {
    //        var res = Composition.SingleOrDefault(comp => comp.ID == compo.ID);
    //        if (res != null)
    //        {
    //            res.Observation = compo.Observation;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateEnfilageTrameX(Enfilage enf)
    //    {
    //        var res = Enfilage.SingleOrDefault(enfila => enfila.ID == enf.ID);
    //        if (res != null)
    //        {
    //            res.TrXposition = enf.TrXposition;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateEnfilageTrameY(Enfilage enf)
    //    {
    //        var res = Enfilage.SingleOrDefault(enfila => enfila.ID == enf.ID);
    //        if (res != null)
    //        {
    //            res.TrYposition = enf.TrYposition;
    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateEnfilageChaine(Enfilage enf)
    //    {
    //        var res = Enfilage.SingleOrDefault(enfila => enfila.ID == enf.ID);
    //        if (res != null)
    //        {
    //            res.GetChaine = enf.GetChaine;
    //            SaveChanges();
    //        }
    //    }

    //    public void RemoveEnfilageChaine(Enfilage enf)
    //    {
    //        var res = Enfilage.SingleOrDefault(enfila => enfila.ID == enf.ID);
    //        if (res != null)
    //        {
    //            res.GetChaine = null;
    //            SaveChanges();
    //        }
    //    }

    //    public int AddNewChaineElement(ChaineMatrix ChaineElement)
    //    {
    //        ChaineMatrix.Add(ChaineElement);
    //        SaveChanges();
    //        return ChaineElement.ID;
    //    }

    //    public int AddNewChaine(chaine NewChaine)
    //    {
    //        Chaine.Add(NewChaine);
    //        SaveChanges();
    //        return NewChaine.ID;
    //    }

    //    public int AddNewEnfilage(Enfilage NewEnfliage)
    //    {
    //        Enfilage.Add(NewEnfliage);
    //        SaveChanges();
    //        return NewEnfliage.ID;
    //    }

    //    public int GetLatestEnfilageMatrixID()
    //    {
    //        if (EnfilageMatrix.Count() > 0)
    //            return EnfilageMatrix.OrderByDescending(en => en.ID).FirstOrDefault().ID + 1;
    //        return 0;
    //    }

    //    public int AddNewEnfilageMatrix(EnfilageMatrix NewEnfMatrix)
    //    {
    //        EnfilageMatrix.Add(NewEnfMatrix);
    //        SaveChanges();
    //        return NewEnfMatrix.ID;
    //    }

    //    public FicheTechnique AddNewFicheTechnique(FicheTechnique NewFiche)
    //    {
    //        FicheTec.Add(NewFiche);
    //        SaveChanges();
    //        return NewFiche;
    //    }

    //    public void AddNewMatiere(Matiere mMatiere)
    //    {
    //        Matiere.Add(mMatiere);
    //        SaveChanges();
    //    }

    //    public void AddNewTitrage(Titrage ti)
    //    {
    //        Titrage.Add(ti);
    //        SaveChanges();
    //    }

    //    public void AddNewMachine(Machine mm)
    //    {
    //        mMachine.Add(mm);
    //        SaveChanges();
    //    }

    //    public void AddNewTypeMatiere(TypeMatiere typeM)
    //    {
    //        TypeMatiere.Add(typeM);
    //        SaveChanges();
    //    }

    //    public void AddNewComposant(Composant comp)
    //    {
    //        Composant.Add(comp);
    //        SaveChanges();
    //    }

    //    public void AddNewColor(Couleur col)
    //    {
    //        Color.Add(col);
    //        SaveChanges();
    //    }

    //    public void AddNewCategorie(Catalogue cat)
    //    {
    //        Catalogue.Add(cat);
    //        SaveChanges();
    //    }

    //    public void AddNewClient(Client client)
    //    {
    //        Client.Add(client);
    //        SaveChanges();
    //    }

    //    public void AddNewConcepteur(Concepteur concept)
    //    {
    //        Concepteur.Add(concept);
    //        SaveChanges();
    //    }

    //    public void AddNewVerificateur(Verificateur veri)
    //    {
    //        Verificateur.Add(veri);
    //        SaveChanges();
    //    }


    //    public void AddNewDuitage(Duitages duit)
    //    {
    //        Duitages.Add(duit);
    //        SaveChanges();
    //    }

    //    public void AddNewDuitageGo(DuitageGomme duit)
    //    {
    //        DuitageGomme.Add(duit);
    //        SaveChanges();
    //    }

    //    public void AddModelMachine(ModelMachine modelM)
    //    {
    //        ModelMachine.Add(modelM);
    //        SaveChanges();
    //    }

    //    public List<ModelMachine> GetModelMachines()
    //    {
    //        return ModelMachine.ToList();
    //    }

    //    public ModelMachine GetModelMachine(ModelMachine modelM)
    //    {
    //        return ModelMachine.FirstOrDefault(model =>
    //            model.NbrBande == modelM.NbrBande && model.MaxWidth == modelM.MaxWidth &&
    //            model.NomModel.Equals(modelM.NomModel));
    //    }

    //    public Duitages GetDuitage(int MachineID, double Duit)
    //    {
    //        return Duitages.FirstOrDefault(duit => duit.Machine.ID == MachineID && duit.Duitage == Duit);
    //    }

    //    public DuitageGomme GetDuitageGo(int MachineID, string Duit)
    //    {
    //        return DuitageGomme.FirstOrDefault(duit => duit.Machine.ID == MachineID && duit.Duitage == Duit);
    //    }

    //    public Machine GetMachine(int Num, ModelMachine model)
    //    {
    //        return mMachine.FirstOrDefault(ma => ma.Num == Num && ma.Model.ID == model.ID);
    //    }

    //    public Matiere GetMatiere(string Ref)
    //    {
    //        return Matiere.FirstOrDefault(ma => ma.Ref.Equals(Ref));
    //    }

    //    public Matiere GetMatiere(Titrage tit, Couleur col)
    //    {
    //        return Matiere.FirstOrDefault(ma => ma.Titrage.ID == tit.ID && ma.GetCouleur.ID == col.ID);
    //    }

    //    public Composant GetComposant(string name)
    //    {
    //        return Composant.FirstOrDefault(co => co.Name.Equals(name));
    //    }

    //    public Couleur GetColor(int id, string name)
    //    {
    //        if (Color.FirstOrDefault(co => co.Nbr == id) != null)
    //            return Color.FirstOrDefault(co => co.Nbr == id);
    //        return Color.FirstOrDefault(co => co.Name.Equals(name));
    //    }

    //    public Couleur CheckUniqueColor(int id, int nbr, string name)
    //    {
    //        if (Color.FirstOrDefault(co => co.Nbr == nbr && co.ID != id) != null)
    //            return Color.FirstOrDefault(co => co.Nbr == nbr && co.ID != id);
    //        return Color.FirstOrDefault(co => co.Name.Equals(name) && co.ID != id);
    //    }

    //    public Catalogue GetCategorie(string name)
    //    {
    //        var cata = Catalogue.FirstOrDefault(co => co.Designation.ToLower().Equals(name.ToLower()));
    //        if (cata != null)
    //            return cata;
    //        return null;
    //    }

    //    public Client GetClient(string name)
    //    {
    //        return Client.FirstOrDefault(co => co.Name.Equals(name));
    //    }

    //    public Verificateur GetVerificateur(string name)
    //    {
    //        return Verificateur.FirstOrDefault(co => co.Name.Equals(name));
    //    }

    //    public Concepteur GetConcepteur(string name)
    //    {
    //        return Concepteur.FirstOrDefault(co => co.Name.Equals(name));
    //    }

    //    public List<Couleur> GetCouleurs(Titrage tit)
    //    {
    //        return Matiere.Include(ma => ma.Titrage).Include(tits => tits.GetCouleur).ToList()
    //            .FindAll(ma => ma.Titrage.ID == tit.ID).Select(ma => ma.GetCouleur).ToList();
    //    }

    //    public List<Catalogue> GetCategories()
    //    {
    //        return Catalogue.ToList();
    //    }

    //    public void EditMatiere(Matiere matiere)
    //    {
    //        var OldMatiere = Matiere.SingleOrDefault(ma => ma.ID == matiere.ID);
    //        if (OldMatiere != null)
    //        {
    //            OldMatiere.Ref = matiere.Ref;
    //            OldMatiere.Titrage = matiere.Titrage;
    //            OldMatiere.Designation = matiere.Designation;
    //            OldMatiere.GetCouleur = matiere.GetCouleur;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditMachine(Machine mm)
    //    {
    //        var OldMachine = mMachine.SingleOrDefault(m => m.ID == mm.ID);
    //        if (OldMachine != null)
    //        {
    //            OldMachine.Num = mm.Num;
    //            OldMachine.Model = mm.Model;
    //            OldMachine.Name = mm.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditComposant(Composant NovComp)
    //    {
    //        var OldComp = Composant.SingleOrDefault(co => co.ID == NovComp.ID);
    //        if (OldComp != null)
    //        {
    //            OldComp.Name = NovComp.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditColor(Couleur NovCol)
    //    {
    //        var OldColor = Color.SingleOrDefault(co => co.ID == NovCol.ID);
    //        if (OldColor != null)
    //        {
    //            OldColor.Nbr = NovCol.Nbr;
    //            OldColor.Name = NovCol.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditCategorie(Catalogue NovCat)
    //    {
    //        var OldCat = Catalogue.SingleOrDefault(cat => cat.ID == NovCat.ID);
    //        if (OldCat != null)
    //        {
    //            OldCat.Designation = NovCat.Designation;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditClient(Client NovClient)
    //    {
    //        var OldClient = Client.SingleOrDefault(co => co.ID == NovClient.ID);
    //        if (OldClient != null)
    //        {
    //            OldClient.Name = NovClient.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditConcepteur(Concepteur NovConcept)
    //    {
    //        var OldConcept = Concepteur.SingleOrDefault(co => co.ID == NovConcept.ID);
    //        if (OldConcept != null)
    //        {
    //            OldConcept.Name = NovConcept.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void EditVerificateur(Verificateur NovVerificateur)
    //    {
    //        var OldVerificateur = Verificateur.SingleOrDefault(co => co.ID == NovVerificateur.ID);
    //        if (OldVerificateur != null)
    //        {
    //            OldVerificateur.Name = NovVerificateur.Name;
    //            SaveChanges();
    //        }
    //    }

    //    public void DeleteMatiere(Machine mm)
    //    {
    //        var mch = mMachine.SingleOrDefault(machin => machin.ID == mm.ID);
    //        if (mch != null)
    //        {
    //            mMachine.Remove(mm);
    //            SaveChanges();
    //        }
    //    }

    //    public void DeleteFicheTechnique(FicheTechnique fiche)
    //    {
    //        var EnfilageIDs = new List<int>();
    //        if (fiche.Produits.Count > 0)
    //            foreach (var prod in fiche.Produits)
    //                if (prod.EnfilageID != null)
    //                    EnfilageIDs.Add(prod.EnfilageID.ID);

    //        var DelFiche = FicheTec.Include(ft => ft.Produits).ThenInclude(pr => pr.GetComposition)
    //            .SingleOrDefault(ft => ft.ID == fiche.ID);

    //        if (DelFiche.Produits.Count > 0)
    //        {
    //            foreach (var prod in DelFiche.Produits)
    //            {
    //                var TempoList = new List<Composition>(prod.GetComposition);
    //                for (var i = 0; i < TempoList.Count; i++)
    //                {
    //                    var DelCompo2 = Composition.SingleOrDefault(compo => compo.ID == TempoList[i].ID);

    //                    Composition.Remove(DelCompo2);
    //                }
    //            }

    //            SaveChanges();
    //        }


    //        foreach (var DelProd in DelFiche.Produits) Produit.Remove(DelProd);

    //        FicheTec.Remove(DelFiche);
    //        SaveChanges();

    //        foreach (var EnfID in EnfilageIDs)
    //        {
    //            var Enf = Enfilage.SingleOrDefault(ef => ef.ID == EnfID);
    //            Enfilage.Remove(Enf);
    //        }

    //        SaveChanges();
    //    }

    //    public void DeleteFicheTechnique(int ficheID, int prodID)
    //    {
    //        var DelFiche = FicheTec.SingleOrDefault(ft => ft.ID == ficheID);
    //        var DelProd = Produit.Include(pr => pr.EnfilageID).SingleOrDefault(pr => pr.Id == prodID);
    //        if (DelProd != null) Enfilage.Remove(DelProd.EnfilageID);

    //        FicheTec.Remove(DelFiche);

    //        SaveChanges();
    //    }

    //    public void DeleteEnfilageElement(int x, int y, int EnfID)
    //    {
    //        var DelEl = EnfilageMatrix.SingleOrDefault(enf => enf.EnfID == EnfID && enf.x == x && enf.y == y);

    //        if (DelEl != null)
    //        {
    //            EnfilageMatrix.Remove(DelEl);

    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateEnfilageElement(int x, int y, int EnfID, Composition content)
    //    {
    //        var UpdateEL = EnfilageMatrix.SingleOrDefault(enf => enf.EnfID == EnfID && enf.x == x && enf.y == y);

    //        if (UpdateEL != null)
    //        {
    //            UpdateEL.value = content;

    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateTrameX(Enfilage Menfilage)
    //    {
    //        var UpdateEnfilage = Enfilage.SingleOrDefault(enf => enf.ID == Menfilage.ID);

    //        if (UpdateEnfilage != null)
    //        {
    //            UpdateEnfilage.TrXposition = Menfilage.TrXposition;

    //            SaveChanges();
    //        }
    //    }

    //    public void UpdateTrameY(Enfilage Menfilage)
    //    {
    //        var UpdateEnfilage = Enfilage.SingleOrDefault(enf => enf.ID == Menfilage.ID);

    //        if (UpdateEnfilage != null)
    //        {
    //            UpdateEnfilage.TrYposition = Menfilage.TrYposition;

    //            SaveChanges();
    //        }
    //    }

    //    public void DeleteMatiere(Matiere mMatiere)
    //    {
    //        Matiere.Remove(mMatiere);
    //        SaveChanges();
    //    }

    //    public void DeleteColor(Couleur col)
    //    {
    //        Color.Remove(col);
    //        SaveChanges();
    //    }

    //    public void DeleteCategorie(Catalogue col)
    //    {
    //        Catalogue.Remove(col);
    //        SaveChanges();
    //    }

    //    public void DeleteClient(Client cli)
    //    {
    //        Client.Remove(cli);
    //        SaveChanges();
    //    }

    //    public void DeleteConcepteur(Concepteur concept)
    //    {
    //        Concepteur.Remove(concept);
    //        SaveChanges();
    //    }

    //    public void DeleteVerificateur(Verificateur veri)
    //    {
    //        Verificateur.Remove(veri);
    //        SaveChanges();
    //    }

    //    public void DeleteComposant(Composant comp)
    //    {
    //        Composant.Remove(comp);
    //        SaveChanges();
    //    }
    }
}