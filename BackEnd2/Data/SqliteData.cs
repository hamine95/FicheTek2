using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using BackEnd2.Database;
using BackEnd2.Model;
using Dapper;
using SQLitePCL;

namespace BackEnd2.Data
{
    public class SqliteData
    {
        
        private readonly string PrincipalFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string connectionStringName;

        private SqliteDataAccess db;
        
        public SqliteData()
        {

            connectionStringName = "Data Source=FicheTeK.db";
            db = new SqliteDataAccess();
        }

        public void DeleteFicheTechnique(int  Ficheid,int Prodid)
        {

            db.SaveData<dynamic>("delete from FicheTechnique where ID=@id", new { id = Ficheid}, connectionStringName);
            db.SaveData<dynamic>("delete from Product where Id=@id", new { id = Prodid}, connectionStringName);

        }
        public void DeleteFicheTechnique(int  Ficheid,int Prodid,int Enfid)
        {
            db.SaveData<dynamic>("delete from Enfilage where ID=@id", new { id = Enfid}, connectionStringName);
            db.SaveData<dynamic>("delete from FicheTechnique where ID=@id", new { id = Ficheid}, connectionStringName);
            db.SaveData<dynamic>("delete from Product where Id=@id", new { id = Prodid}, connectionStringName);
            

        }
        public List<user> GetUsers()
        {
          return  db.LoadData<user,dynamic>("select * from user,usertype where user.type==usertype.ID", null, connectionStringName);
            
        }

        public Machine GetMachine(int num,ModelMachine model)
        {
            List<Machine> machlist = db.LoadData<Machine, dynamic>(
                "select * from mMachine where ModelID=@model and Num=@num", new { num = num, model = model.ID },
                connectionStringName);
                

            if (machlist.Count > 0)
            {
               return machlist[0];
            }
            else
            {
                return null;
            }
        }
        public void AddNewMachine(Machine mach)
        {
            string stm = "Insert into mMachine(Num,Name,ModelID,DoubleDuitage) values(@num,@name,@model,@dub)";
            db.SaveData(stm, new { num = mach.Num, name = mach.Name, model = mach.Model.ID,dub=mach.DoubleDuitage }, connectionStringName);
        }
        public void ValidateFicheTechnique(Produit pr)
        {
            db.SaveData<dynamic>("Update Product set Definite=1 where Product.Id=@id",new {pr.Id},connectionStringName);
            
        }

        public List<Reed> GetPeigneList()
        {
            return db.LoadData<Reed, dynamic>("select * from Reed ",null,connectionStringName);
        }

        public void AjouterNouveauPeigne(Reed rd)
        {
            db.SaveData<dynamic>("insert into Reed(Nombre) values(@Numero)",new {Numero=rd.Nombre},connectionStringName);
        }
        public int AddNewProduct(Produit NewProd)
        {

            string sqlStm = "insert into Product(Ref,Version,FicheId,Definite,Name,IsEnfilage,Redaction,DateCreation) values(@Ref,@Version,@FicheId,@Definite,@Name,@IsEnf,CURRENT_TIMESTAMP,@datecr)";
          NewProd.Id=Convert.ToInt32(db.SaveData(sqlStm,new {id=NewProd.Id,Ref=NewProd.Ref,Version=NewProd.Version,Definite=NewProd.Definite,FicheId=NewProd.FicheId,Name=NewProd.Name,IsEnf=NewProd.IsEnfilage,datecr=NewProd.DateCreation},connectionStringName));
            return NewProd.Id;
        }
        public int AddNewProductVersion(Produit NewProd)
        {

            string sqlStm = "insert into Product(Ref,Version,NumArticle,DateCreation,Dent,Largeur,FicheId,Definite,Name,IsEnfilage,Redaction) values(@Ref,@Version,@numAr,@datecr,@dent,@larg,@FicheId,@Definite,@Name,@IsEnf,CURRENT_TIMESTAMP)";
            NewProd.Id=Convert.ToInt32(db.SaveData(sqlStm,new {id=NewProd.Id,Ref=NewProd.Ref,Version=NewProd.Version,Definite=0,
                datecr=NewProd.DateCreation,dent=NewProd.Dent,larg=@NewProd.Largeur,numAr=NewProd.NumArticle,FicheId=NewProd.FicheId,Name=NewProd.Name,IsEnf=NewProd.IsEnfilage},connectionStringName));
            return NewProd.Id;
        }
        public void UpdateProdVersion(Produit NewProd)
        {
            string stm = "Update Product set Version=@ver where Id=@id";
            db.SaveData(stm, new { ver = NewProd.Version, id = NewProd.Id }, connectionStringName);

        }
        public void UpdateProdImage(Produit NewProd)
        {
            string stm = "Update Product set image=@img where Id=@id";
            db.SaveData(stm, new { img = NewProd.image, id = NewProd.Id }, connectionStringName);

        }
        public void UpdateProdDuitage(Produit NewProd)
        {
            string stm = "Update Product set DuitageIDID=@duit where Id=@id";
            db.SaveData(stm, new { duit = NewProd.DuitageID.ID, id = NewProd.Id }, connectionStringName);
            
        }
        public void UpdateProdEnfilage(Produit NewProd)
        {
            string stm = "Update Product set EnfilageIDID=@enf where Id=@id";
            db.SaveData(stm, new { enf = NewProd.EnfilageID.ID, id = NewProd.Id }, connectionStringName);
            
        }
        public void UpdateProdDent(Produit NewProd)
        {
            string stm = "Update Product set Dent=@dent where Id=@id";
            db.SaveData(stm, new { dent = NewProd.Dent, id = NewProd.Id }, connectionStringName);
  
        }
      
        public void UpdateProdDuitageGomme(Produit NewProd)
        {
            string stm = "Update Product set DuitageGommeID=@duit where Id=@id";
            db.SaveData(stm, new { duit = NewProd.DuitageGomme.ID, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdNumArticle(Produit NewProd)
        {
            string stm = "Update Product set NumArticle=@num where Id=@id";
            db.SaveData(stm, new { num = NewProd.NumArticle, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdRef(Produit NewProd)
        {
            string stm = "Update Product set Ref=@refe where Id=@id";
            db.SaveData(stm, new { refe = NewProd.Ref, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateEnfDent(Produit NewProd)
        {
            string stm = "Update Product set EnfDent=@enfDent where Id=@id";
            db.SaveData(stm, new { enfDent = NewProd.EnfDent, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdName(Produit NewProd)
        {
            string stm = "Update Product set Name=@name where Id=@id";
            db.SaveData(stm, new { name = NewProd.Name, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdConcepteur(Produit NewProd)
        {
            string stm = "Update Product set ConcepteurID=@concept where Id=@id";
            db.SaveData(stm, new { concept = NewProd.Concepteur.ID, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdVerificateur(Produit NewProd)
        {
            string stm = "Update Product set VerificateurID=@ver where Id=@id";
            db.SaveData(stm, new { ver = NewProd.Verificateur.ID, id = NewProd.Id }, connectionStringName);

            
        }
        public int AddProdCompo(Composition compo)
        {
            string stm = "Insert into Composition(BKBorderComposant,BKComposant,NumComposant,FGComposant,DebutFil,Intermittent,Num"+
                         ",NbrFil,Torsion,Enfilage,Emb,Poids,Observation,ImagePath,ImageReedPath,EnfNbrFil,ProdIDId) values "+
                         "(@bkb,@bk,@numComp,@fg,@df,@inter,@num,@nbrfil,@tor,@enf,@emb,@poids,@obs,@img,@imgReed,@EnfNbr,@prodid)";
           int NewID= Convert.ToInt32(db.SaveData(stm, new { bkb = compo.BKBorderComposant, bk = compo.BKComposant,
                numComp=compo.NumComposant,
                fg=compo.FGComposant,df=compo.DebutFil,inter=compo.Intermittent,num=compo.Num,nbrfil=compo.NbrFil,tor=compo.Torsion
                ,enf=compo.Enfilage,emb=compo.Emb,poids=compo.Poids,obs=compo.Observation,img=compo.ImagePath,imgReed=compo.ImageReedPath,
                EnfNbr=compo.EnfNbrFil,prodid=compo.ProdID.Id
            }, connectionStringName));
           return NewID;

        }
        public void UpdateCompoComposant(Composition compo)
        {
            string stm = "Update Composition set GetComposantID=@cmp where ID=@id";
            db.SaveData(stm, new { cmp = compo.GetComposant.ID, id = compo.ID }, connectionStringName);

            
        }
        public void UpdateCompoMatiere(Composition compo)
        {
            string stm = "Update Composition set GetMatiereID=@mat where ID=@id";
            db.SaveData(stm, new { mat = compo.GetMatiere.ID, id = compo.ID }, connectionStringName);

            
        }
        public void UpdateProdRedacteur(Produit NewProd)
        {
            string stm = "Update Product set Redacteur=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.RedacteurObj.ID, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdRedaction(Produit NewProd)
        {
            string stm = "Update Product set Redaction=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.Redaction, id = NewProd.Id }, connectionStringName);
            
        }
        public void UpdateProdDateCreation(Produit NewProd)
        {
            string stm = "Update Product set DateCreation=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.DateCreation, id = NewProd.Id }, connectionStringName);

        }

        public void UpdateChColComp(ChColComp chCol)
        {
            string stm = "Update ChColComp set CompsantID=@compid where ChaineID=@chid and ColNum=@colNum";

            db.SaveData(stm,
                new
                {

                    compid=chCol.ComposantID,
                    chid=chCol.ChaineID,
                    colNum=chCol.ColNum
                }, connectionStringName);
        }
        public void AddChColComp(ChColComp chCol)
        {
            string stm = "Insert into ChColComp(ChaineID,ColNum,ComposantID) values(@chid,@colnum,@compid)";

            db.SaveData(stm,
                new
                {

                    compid=chCol.ComposantID,
                    chid=chCol.ChaineID,
                    colnum=chCol.ColNum
                }, connectionStringName);
        }
        public List<ChColComp> GetChColComps(int ChID)
        {
            string stm = "Select * from ChColComp where ChaineID=@ChID";
            List<ChColComp> chcollist= db.LoadData<ChColComp, dynamic>(stm, new
            {

                ChID=ChID,
            }, connectionStringName);
           
                return chcollist;
          
        }
        public ChColComp GetChColComp(int ChID, int ColNum)
        {
            string stm = "Select * from ChColComp where ChaineID=@ChID and ColNum=@ColNum";
          List<ChColComp> chcollist= db.LoadData<ChColComp, dynamic>(stm, new
            {

                ChID=ChID,
                ColNum=ColNum,
            }, connectionStringName);
          if (chcollist.Count > 0)
          {
              return chcollist[0];
          }
          else
          {
              return null;
          }
        }
        public List<Composant> GetComposants()
        {
            string stm = "Select * from Composant";
           return db.LoadData<Composant, dynamic>(stm, null, connectionStringName);
        }
        public void UpdateProdMiseAJour(Produit NewProd)
        {
            string stm = "Update Product set MiseAJour=@mise where Id=@id";
            db.SaveData(stm, new { mise = NewProd.MiseAJour, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdName2(Produit NewProd)
        {
            string stm = "Update Product set Name2=@name where Id=@id";
            db.SaveData(stm, new { name = NewProd.Name2, id = NewProd.Id }, connectionStringName);

            
        }
        public bool CheckVersionUnique(Produit NewProd)
        {

            string stm = "select * from Product as pr,FicheTechnique as ft where pr.FicheId=ft.ID and Version=@ver and ft.ID=@id";
            List<Produit> prodlist = db.LoadData<Produit, dynamic>(stm, new { ver = NewProd.Version, id = NewProd.FicheId },
                connectionStringName);
            if (prodlist.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool CheckNArticleUnique(int NumArticle,int ProdID)
        { 
            string stm = "select * from Product where NumArticle=@num and Id!=@id";
            List<Produit> prodlist = db.LoadData<Produit, dynamic>(stm, new { num = NumArticle, id = ProdID },
                connectionStringName);
            if (prodlist.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }

         
          
        }
        public void UpdateProdClient(Produit NewProd)
        {
            string stm = "Update Product set ClientID=@cl where Id=@id";
            db.SaveData(stm, new { id = NewProd.Id, cl = NewProd.Client.ID }, connectionStringName);

        }
        public void UpdateProdLargeur(Produit NewProd)
        {
            string stm = "Update Product set Largeur=@larg where Id=@id";
            db.SaveData(stm, new { larg = NewProd.Largeur, id = NewProd.Id }, connectionStringName);

            
        }
        public void UpdateProdEpaiseur(Produit NewProd)
        {
            string stm = "Update Product set Epaisseur=@ep where Id=@id";
            db.SaveData(stm, new { ep = NewProd.Epaisseur, id = NewProd.Id }, connectionStringName);

        }
        public void UpdateProdIsEnfilage(Produit NewProd)
        {
            string stm = "Update Product set IsEnfilage=@IsEnf where Id=@id";
            db.SaveData(stm, new { IsEnf = NewProd.IsEnfilage, id = NewProd.Id }, connectionStringName);

            
        }
        public int AddNewProductWithEnfilage(Produit NewProd)
        {

            string sqlStm = "insert into Product(Id,Ref,Version,FicheId,Definite,Name,IsEnfilage,EnfilageIDID,Redaction,DateCreation) values(@id,@Ref,@Version,@FicheId,@Definite,@Name,@IsEnf,@EnfID,CURRENT_TIMESTAMP,@datecr)";
            NewProd.Id=Convert.ToInt32(db.SaveData(sqlStm,new {id=NewProd.Id,Ref=NewProd.Ref,Version=NewProd.Version,Definite=NewProd.Definite,FicheId=NewProd.FicheId,Name=NewProd.Name,IsEnf=NewProd.IsEnfilage,EnfID=NewProd.EnfilageID.ID,datecr=NewProd.DateCreation},connectionStringName));
            return NewProd.Id;
        }
        public void UpdateFicheTechniqueCategorie(int FtID,Catalogue cat)
        {
            string stm = "Update FicheTechnique set CatalogID=@catID where ID=@id";
            db.SaveData<dynamic>(stm, new { catID = cat.ID, id = FtID }, connectionStringName);
           
            
        }
        public void DeletePeigne(Reed rd)
        {
            db.SaveData<dynamic>("Delete from  Reed  where ID=@id",new {id=rd.ID},connectionStringName);
        }
        public void ModifierNouveauPeigne(Reed rd)
        {
            db.SaveData<dynamic>("update  Reed set Nombre=@num where ID=@id",new {Numero=rd.Nombre,id=rd.ID},connectionStringName);
        }
        public void RemoveEnfilage(Enfilage mEnfilage)
        {

            string stm = "Delete from Enfilage where ID=@id";
            db.SaveData(stm, new { id = mEnfilage.ID }, connectionStringName);

        }
        public int AddNewEnfilage(Enfilage NewEnfliage)
        {
           
            return Convert.ToInt32(db.SaveData<dynamic>("insert into Enfilage(TrXposition,TrYposition,Column,Row,NbrDent) values(@Trx,@Try,@col,@row,@Dent)",new {Trx=NewEnfliage.TrXposition,Try=NewEnfliage.TrYposition,col=NewEnfliage.Column,row=NewEnfliage.Row,Dent=NewEnfliage.NbrDent},connectionStringName));
        }
        public int GetLastProductID()
        {
           var obj= db.LoadData<Produit, dynamic>("Select * from Product",null,connectionStringName);
           if (obj != null && obj.Count>0)
           {
               List<int> prlist= db.LoadData<int, dynamic>("Select max(Id) from Product order by Id desc",null,connectionStringName);
               if (prlist!=null && prlist.Count > 0)
               {
                   return prlist[0];
               }
               else
               {
                   return 0;
               }
           }
           else
           {
               return 0;
           }
          
          
          
        }
        
        public FicheTechnique AddNewFicheTechnique(FicheTechnique NewFiche)
        {


            string SqlStm = "Insert into FicheTechnique(ModelFiche,IsArchive) values(@model,@Arch)";
            int id= Convert.ToInt32(db.SaveData<dynamic>(SqlStm,new {model=NewFiche.ModelFiche,Arch=NewFiche.IsArchive},connectionStringName));

          List<FicheTechnique> ft= db.LoadData<FicheTechnique, dynamic>("Select * from FicheTechnique where ID=@id", new {id=id}, connectionStringName);
          return ft[0];
        }
        public void UpdateProdPeigne(Produit pr)
        {
            
            db.SaveData<dynamic>("Update Product set Peigne=@reed where Product.Id=@id",new {reed=pr.PeigneObj.ID,id=pr.Id},connectionStringName);

        }
        public void UpdateEnfilageChaine(Enfilage enf)
        {
            string stm = "Update Enfilage set GetChaineID=@chid where ID=@id";
            db.SaveData(stm, new { id = enf.ID ,chid=enf.GetChaine.ID}, connectionStringName);
            
            
        }
        public List<chaine> GetChaines()
        {
           
            string SqlStm = "Select * from Chaine as ch";             

            List<chaine> chlist= db.LoadData<chaine,dynamic>(SqlStm,null,connectionStringName);
            foreach (var ch in chlist)
            {
                SqlStm = "Select * from ChaineMatrix as chmat where ChaineID=@id";
                ch.ChMatrix= db.LoadData<ChaineMatrix, dynamic>(SqlStm, new { id = ch.ID }, connectionStringName);
                using (var  conn=new SQLiteConnection(connectionStringName))
                {
                    SqlStm = "Select * from ChColComp as chc,Composant as cmp where  chc.ComposantID=cmp.ID and chc.ChaineID=@chid";             
                              
                    var result =  conn.Query<ChColComp,Composant,ChColComp>(SqlStm, (chc, cmp) =>
                    {
                        chc.Comp= cmp;
                        return chc;
                    },new {chid=ch.ID});
 
                    ch.ChaineCompos=  result.AsList();
                }
            }

            return chlist;
        }
        public List<Machine> GetCrochtageMachines()
        {
            string sqlStm = "Select * from mMachine where DoubleDuitage=1";
            return db.LoadData<Machine, dynamic>(sqlStm, null, connectionStringName);
        }
        public Produit GetFullProduct(Produit pr)
        {
           
                foreach (var compo in pr.GetComposition)
                {
                    string stm1 =
                        "Select * from Composition as compo,Matiere as mat where compo.GetMatiereID=mat.ID and compo.ID=@id";
                    List<Matiere> matlist= db.LoadData<Matiere,dynamic>(stm1, new { id = compo.ID }, connectionStringName);

                    if (matlist.Count > 0)
                    {
                        compo.GetMatiere = matlist[0];
                        
                        stm1 =
                            "Select tit.Designation,tit.ID,tit.NumMetric,tit.NumTwist,tit.TypeMatiereID  from Titrage as tit,Matiere as mat where mat.TitrageID=tit.ID and mat.ID=@id";
                        List<Titrage> titlist= db.LoadData<Titrage,dynamic>(stm1, new { id = compo.GetMatiere.ID }, connectionStringName);

                        if (titlist.Count > 0)
                        {
                            compo.GetMatiere.Titrage = titlist[0];
                        
                            stm1 =
                                "Select * from Titrage as tit,TypeMatiere as mattyp where tit.TypeMatiereID=mattyp.ID and tit.ID=@id";
                            List<TypeMatiere> typematlist= db.LoadData<TypeMatiere,dynamic>(stm1, new { id = compo.GetMatiere.Titrage.ID }, connectionStringName);

                            if (typematlist.Count > 0)
                            {
                                compo.GetMatiere.Titrage.TypeMatiere = typematlist[0];
                            }
                            
                          
                        }
                        stm1 =
                            "Select * from Matiere as mat,Color as col where mat.GetCouleurID=col.ID and mat.ID=@id";
                        List<Couleur> couleurlist= db.LoadData<Couleur,dynamic>(stm1, new { id = compo.GetMatiere.ID }, connectionStringName);

                        if (couleurlist.Count > 0)
                        {
                            compo.GetMatiere.GetCouleur = couleurlist[0];
                        }
                    }
                    
                }

                string stm = "Select cl.ID,cl.Name from Client as cl,Product as pr where pr.ClientID=cl.ID and pr.Id=@id";
                List<Client> ctlist=
                    db.LoadData<Client,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                if (ctlist.Count > 0)
                {
                    pr.Client = ctlist[0];
                }
                
                 stm = "Select duit.ID,duit.Duitage,duit.Vitesse from Duitages as duit,Product as pr where pr.DuitageIDID=duit.ID and pr.Id=@id";
                List<Duitages> duitlist=
                    db.LoadData<Duitages,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                if (duitlist.Count > 0)
                {
                    pr.DuitageID = duitlist[0];
                    
                    stm = "Select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and duit.ID=@id";
                    List<Machine> machlist=
                        db.LoadData<Machine,dynamic>(stm, new { id = pr.DuitageID.ID }, connectionStringName);

                    if (machlist.Count > 0)
                    {
                        pr.DuitageID.Machine = machlist[0];
                    }
                    
                }
                
                stm = "Select duit.ID,duit.Duitage,duit.Vitesse from DuitageGomme as duit,Product as pr where pr.DuitageGommeID=duit.ID and pr.Id=@id";
                List<DuitageGomme> duitgolist=
                    db.LoadData<DuitageGomme,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                if (duitgolist.Count > 0)
                {
                    pr.DuitageGomme = duitgolist[0];
                }
                stm = "Select cp.ID,cp.Name from Concepteur as cp,Product as pr where pr.ConcepteurID=cp.ID and pr.Id=@id";
                 List<Concepteur> conceptList=
                    db.LoadData<Concepteur,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                 if (conceptList.Count > 0)
                 {
                     pr.Concepteur = conceptList[0];
                 }
                 stm = "Select pn.ID,pn.Nombre from Reed as pn,Product as pr where pr.Peigne=pn.ID and pr.Id=@id";
                 List<Reed> reedList=
                     db.LoadData<Reed,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                 if (reedList.Count > 0)
                 {
                     pr.PeigneObj = reedList[0];
                 }
                 stm = "Select rd.ID,rd.Name from Redacteur as rd,Product as pr where pr.Redacteur=rd.ID and pr.Id=@id";
                 List<Redacteur> redacteurList=
                     db.LoadData<Redacteur,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                 if (redacteurList.Count > 0)
                 {
                     pr.RedacteurObj = redacteurList[0];
                 }
                 
                stm = "Select ver.ID,ver.Name from Verificateur as ver,Product as pr where pr.VerificateurID=ver.ID and pr.Id=@id";
                List<Verificateur> verlist=
                    db.LoadData<Verificateur,dynamic>(stm, new { id = pr.Id }, connectionStringName);

                if (verlist.Count > 0)
                {
                    pr.Verificateur = verlist[0];
                }
                if (pr.IsEnfilage == 1)
                {
                    stm = "Select enf.ID,enf.GetChaineID,enf.TrXposition,enf.TrYposition,enf.Column,enf.Row,enf.NbrDent from Enfilage as enf,Product as pr where pr.EnfilageIDID=enf.ID and pr.Id=@id";
                    List<Enfilage> enflist=
                        db.LoadData<Enfilage,dynamic>(stm, new { id = pr.Id }, connectionStringName);
                    if (enflist.Count > 0)
                    {
                        pr.EnfilageID = enflist[0];
                        stm = "Select * from Enfilage as enf,Chaine as ch where ch.ID=enf.GetChaineID and enf.ID=@id";
                        List<chaine> chlist=
                            db.LoadData<chaine,dynamic>(stm, new { id = pr.EnfilageID.ID }, connectionStringName);

                        if (chlist.Count > 0)
                        {
                            pr.EnfilageID.GetChaine = chlist[0];
                        }
                        if (pr.EnfilageID.GetChaine != null)
                        {
                            
                            using (var conn = new SQLiteConnection(connectionStringName))
                            {
                                stm = "Select * from ChColComp as ch,Composant as cmp where ch.ComposantID=cmp.ID and ch.ChaineID=@chid";
                                
                              
                                var result =  conn.Query<ChColComp,Composant,ChColComp>(stm, (ch, cmp) =>
                                {
                                    ch.Comp= cmp;
                                    return ch;
                                },new {chid=pr.EnfilageID.GetChaine.ID});
 
                                pr.EnfilageID.GetChaine.ChaineCompos =  result.AsList();
                            }
                            stm = "Select * from ChaineMatrix as chmat,Chaine as ch where chmat.ChaineID=ch.ID and ch.ID=@id";
                            pr.EnfilageID.GetChaine.ChMatrix =
                                db.LoadData<ChaineMatrix,dynamic>(stm, new { id = pr.EnfilageID.GetChaine.ID }, connectionStringName);

                        }
                   
                        stm = "Select * from Enfilage as enf,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enf.ID=@id";
                        pr.EnfilageID.GetMatrix =
                            db.LoadData<EnfilageMatrix,dynamic>(stm, new { id = pr.EnfilageID.ID }, connectionStringName);

                        using (var conn = new SQLiteConnection(connectionStringName))
                        {
                            stm = "Select " +
                                  "enf.ID,enfmat.ID,enfmat.x,enfmat.y,enfmat.valueID,enfmat.DentFil,enfmat.EnfID" +
                                  ",cmp.ID,cmp.BKBorderComposant,cmp.BKComposant,cmp.NumComposant,cmp.FGComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                                  ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId,cmp.ImagePath,cmp.ImageReedPath" +
                                  ",cmp.EnfNbrFil" +
                                  " from Enfilage as enf,Composition as cmp,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enfmat.valueID=cmp.ID and enf.ID=@id";
                            var result =  conn.Query<EnfilageMatrix,Composition,EnfilageMatrix>(stm, (enfmat, cmp) =>
                            {
                                enfmat.value= cmp;
                                return enfmat;
                            },new {id=pr.EnfilageID.ID});
 
                            pr.EnfilageID.GetMatrix = result.AsList();
                        }
                    }

                   


                }
           

                return pr;
        }

         public FicheTechnique GetFullFicheTechnique(FicheTechnique ft,int ind)
        {
            if (ft.Produits.Count > 0)
            {
               int lastIndex= ind;
                foreach (var compo in ft.Produits[lastIndex].GetComposition)
                {
                    string stm1 =
                        "Select * from Composition as compo,Matiere as mat where compo.GetMatiereID=mat.ID and compo.ID=@id";
                    List<Matiere> matlist= db.LoadData<Matiere,dynamic>(stm1, new { id = compo.ID }, connectionStringName);

                    if (matlist.Count > 0)
                    {
                        compo.GetMatiere = matlist[0];
                        
                        stm1 =
                            "Select tit.Designation,tit.ID,tit.NumMetric,tit.NumTwist,tit.TypeMatiereID  from Titrage as tit,Matiere as mat where mat.TitrageID=tit.ID and mat.ID=@id";
                        List<Titrage> titlist= db.LoadData<Titrage,dynamic>(stm1, new { id = compo.GetMatiere.ID }, connectionStringName);

                        if (titlist.Count > 0)
                        {
                            compo.GetMatiere.Titrage = titlist[0];
                        
                            stm1 =
                                "Select * from Titrage as tit,TypeMatiere as mattyp where tit.TypeMatiereID=mattyp.ID and tit.ID=@id";
                            List<TypeMatiere> typematlist= db.LoadData<TypeMatiere,dynamic>(stm1, new { id = compo.GetMatiere.Titrage.ID }, connectionStringName);

                            if (typematlist.Count > 0)
                            {
                                compo.GetMatiere.Titrage.TypeMatiere = typematlist[0];
                            }
                            
                          
                        }
                        stm1 =
                            "Select * from Matiere as mat,Color as col where mat.GetCouleurID=col.ID and mat.ID=@id";
                        List<Couleur> couleurlist= db.LoadData<Couleur,dynamic>(stm1, new { id = compo.GetMatiere.ID }, connectionStringName);

                        if (couleurlist.Count > 0)
                        {
                            compo.GetMatiere.GetCouleur = couleurlist[0];
                        }
                    }
                    
                }

                string stm = "Select cl.ID,cl.Name from Client as cl,Product as pr where pr.ClientID=cl.ID and pr.Id=@id";
                List<Client> ctlist=
                    db.LoadData<Client,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                if (ctlist.Count > 0)
                {
                    ft.Produits[lastIndex].Client = ctlist[0];
                }
                
                 stm = "Select duit.ID,duit.Duitage,duit.Vitesse from Duitages as duit,Product as pr where pr.DuitageIDID=duit.ID and pr.Id=@id";
                List<Duitages> duitlist=
                    db.LoadData<Duitages,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                if (duitlist.Count > 0)
                {
                    ft.Produits[lastIndex].DuitageID = duitlist[0];
                    
                    stm = "Select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and duit.ID=@id";
                    List<Machine> machlist=
                        db.LoadData<Machine,dynamic>(stm, new { id = ft.Produits[lastIndex].DuitageID.ID }, connectionStringName);

                    if (machlist.Count > 0)
                    {
                        ft.Produits[lastIndex].DuitageID.Machine = machlist[0];
                    }
                    
                }
                stm = "Select cp.ID,cp.Name from Concepteur as cp,Product as pr where pr.ConcepteurID=cp.ID and pr.Id=@id";
                 List<Concepteur> conceptList=
                    db.LoadData<Concepteur,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                 if (conceptList.Count > 0)
                 {
                     ft.Produits[lastIndex].Concepteur = conceptList[0];
                 }
                 stm = "Select pn.ID,pn.Nombre from Reed as pn,Product as pr where pr.Peigne=pn.ID and pr.Id=@id";
                 List<Reed> reedList=
                     db.LoadData<Reed,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                 if (reedList.Count > 0)
                 {
                     ft.Produits[lastIndex].PeigneObj = reedList[0];
                 }
                 stm = "Select rd.ID,rd.Name from Redacteur as rd,Product as pr where pr.Redacteur=rd.ID and pr.Id=@id";
                 List<Redacteur> redacteurList=
                     db.LoadData<Redacteur,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                 if (redacteurList.Count > 0)
                 {
                     ft.Produits[lastIndex].RedacteurObj = redacteurList[0];
                 }
                 
                stm = "Select ver.ID,ver.Name from Verificateur as ver,Product as pr where pr.VerificateurID=ver.ID and pr.Id=@id";
                List<Verificateur> verlist=
                    db.LoadData<Verificateur,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);

                if (verlist.Count > 0)
                {
                    ft.Produits[lastIndex].Verificateur = verlist[0];
                }
                if (ft.Produits[lastIndex].IsEnfilage == 1)
                {
                    stm = "Select enf.ID,enf.GetChaineID,enf.TrXposition,enf.TrYposition,enf.Column,enf.Row,enf.NbrDent from Enfilage as enf,Product as pr where pr.EnfilageIDID=enf.ID and pr.Id=@id";
                    List<Enfilage> enflist=
                        db.LoadData<Enfilage,dynamic>(stm, new { id = ft.Produits[lastIndex].Id }, connectionStringName);
                    if (enflist.Count > 0)
                    {
                        ft.Produits[lastIndex].EnfilageID = enflist[0];
                        stm = "Select * from Enfilage as enf,Chaine as ch where ch.ID=enf.GetChaineID and enf.ID=@id";
                        List<chaine> chlist=
                            db.LoadData<chaine,dynamic>(stm, new { id = ft.Produits[lastIndex].EnfilageID.ID }, connectionStringName);

                        if (chlist.Count > 0)
                        {
                            ft.Produits[lastIndex].EnfilageID.GetChaine = chlist[0];
                        }
                        if (ft.Produits[lastIndex].EnfilageID.GetChaine != null)
                        {
                            stm = "Select * from ChaineMatrix as chmat,Chaine as ch where chmat.ChaineID=ch.ID and ch.ID=@id";
                            ft.Produits[lastIndex].EnfilageID.GetChaine.ChMatrix =
                                db.LoadData<ChaineMatrix,dynamic>(stm, new { id = ft.Produits[lastIndex].EnfilageID.GetChaine.ID }, connectionStringName);

                        }
                   
                        
                        // ft.Produits[lastIndex].EnfilageID.GetMatrix =
                        //     db.LoadData<EnfilageMatrix,dynamic>(stm, new { id = ft.Produits[lastIndex].EnfilageID.ID }, connectionStringName);

                        using (var conn = new SQLiteConnection(connectionStringName))
                        {
                            stm = "Select " +
                                  "enf.ID,enfmat.ID,enfmat.x,enfmat.y,enfmat.valueID,enfmat.DentFil,enfmat.EnfID" +
                                  ",cmp.ID,cmp.BKBorderComposant,cmp.BKComposant,cmp.NumComposant,cmp.FGComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                                  ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId,cmp.ImagePath,cmp.ImageReedPath" +
                                  ",cmp.EnfNbrFil" +
                                  " from Enfilage as enf,Composition as cmp,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enfmat.valueID=cmp.ID and enf.ID=@id";
                            var result =  conn.Query<EnfilageMatrix,Composition,EnfilageMatrix>(stm, (enfmat, cmp) =>
                            {
                                enfmat.value= cmp;
                                return enfmat;
                            },new {id=ft.Produits[lastIndex].EnfilageID.ID});
 
                            ft.Produits[lastIndex].EnfilageID.GetMatrix = result.AsList();
                        }
                    }

                   
                }
            }


            return ft;
        }
         public void AssignOrderToFicheTechnique(int CatID)
         {
             
             string stm = "Select max(pr.Version) as Version,pr.FicheId,pr.Name,pr.Largeur,pr.Epaisseur from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and ft.CatalogID=@catid group by pr.FicheId ORDER by Largeur,Epaisseur,Name";
             List<Produit> prlist= db.LoadData<Produit, dynamic>(stm, new { catid = @CatID }, connectionStringName);
             for (int i=0;i<prlist.Count;i++)
             {
                 int ordre = i + 1;
                 string stm2 = "Update FicheTechnique set Ordre=@ordre where ID=@fichId";
                 db.SaveData(stm2, new { FichId = prlist[i].FicheId,ordre= ordre}, connectionStringName);
                
             }
         }
         public int AssignOrderToFicheTechnique(int FicheID,int CatID)
         {
             int ord = 1;
           string stm = "Select max(pr.Version) as Version,pr.FicheId,pr.Name,pr.Largeur,pr.Epaisseur from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and ft.CatalogID=@catid group by pr.FicheId ORDER by Largeur,Epaisseur,Name";
           List<Produit> prlist= db.LoadData<Produit, dynamic>(stm, new { catid = @CatID }, connectionStringName);
           for (int i=0;i<prlist.Count;i++)
           {
               int ordre = i + 1;
               string stm2 = "Update FicheTechnique set Ordre=@ordre where ID=@fichId";
               db.SaveData(stm2, new { FichId = prlist[i].FicheId,ordre= ordre}, connectionStringName);
               if (prlist[i].FicheId == FicheID)
               {
                   ord = ordre;
               }
           }

           return ord;
         }
         public  List<Catalogue> GetCategorieChildren(int CatID)
         {
             string stm = "Select * from Catalogue where parent=@catID";
             List<Catalogue> listcat= db.LoadData<Catalogue, dynamic>(stm, 
                 new
                 {
                     catID=CatID,
                 }, connectionStringName);
           

             return listcat;
         }
         public  List<Catalogue> GetRootCategories()
         {
             string stm = "Select * from Catalogue where parent=-1";
             List<Catalogue> listcat= db.LoadData<Catalogue, dynamic>(stm, null, connectionStringName);
             

             return listcat;
         }
         public  List<Catalogue> GetCategoriesWithoutChildren()
         {
             string stm = "Select * from Catalogue";
             List<Catalogue> PureCatList = new List<Catalogue>();
            List<Catalogue> listcat= db.LoadData<Catalogue, dynamic>(stm, null, connectionStringName);
            foreach (var cat in listcat)
            {
               Catalogue HaveChild= listcat.SingleOrDefault(cata => cata.parent == cat.ID);
               if (HaveChild == null)
               {
                   PureCatList.Add(cat);
               }
            }

            return PureCatList;
         }
        public void AddNewRedacteur(Redacteur red)
        {
            db.SaveData<dynamic>("Insert into Redacteur(Name) values(@name)", new { name = red.Name },
                connectionStringName);
        }

        public void EditRedacteur(Redacteur red)
        {
            db.SaveData<dynamic>("Update Redacteur set Name=@name where ID=@id", new { name = red.Name, id = red.ID },
                connectionStringName);
        }
        public Redacteur GetRedacteur(string nameRed)
        {

            List<Redacteur> redlist= db.LoadData<Redacteur, dynamic>("Select * from Redacteur where Name=@name", new {name=nameRed}, connectionStringName);
            if (redlist.Count > 0)
            {
                return redlist[0];
            }
            else
            {
                return null;
            }
        }

        public List<ModelMachine> GetModelMachines()
        {
          return  db.LoadData<ModelMachine, dynamic>("Select * from ModelMachine", null, connectionStringName);
        }
        public void AddNewDuitage(Duitages duita)
        {
            string stm = "Insert into Duitages(MachineID,Duitage,Vitesse) values(@mach,@duit,@vit)";
            db.SaveData<dynamic>(stm, new { mach = duita.Machine.ID, duit = duita.Duitage,vit=duita.Vitesse }, connectionStringName);
        }
        public void AddNewDuitageGo(DuitageGomme duita)
        {
            string stm = "Insert into DuitageGomme(MachineID,Duitage) values(@mach,@duit)";
            db.SaveData<dynamic>(stm, new { mach = duita.Machine.ID, duit = duita.Duitage }, connectionStringName);
        }
        public Duitages GetDuitage(int MachID, double duit)
        {
            string stm = "Select * from Duitages where MachineID=@id and Duitage=@d";
           List<Duitages> duitlist= db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit }, connectionStringName);
           if (duitlist.Count > 0)
           {
               return duitlist[0];
           }
           else
           {
               return null;
           }
        }
        public Duitages GetDuitageEdit(int MachID, double duit,double Vitesse)
        {
            string stm = "Select * from Duitages where MachineID=@id and Duitage=@d and Vitesse=@vit";
            List<Duitages> duitlist= db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit ,vit=Vitesse}, connectionStringName);
            if (duitlist.Count > 0)
            {
                return duitlist[0];
            }
            else
            {
                return null;
            }
        }
        public Duitages GetDuitageGoEdit(int MachID, string duit)
        {
            string stm = "Select * from DuitageGomme where MachineID=@id and Duitage=@d ";
            List<Duitages> duitlist= db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit }, connectionStringName);
            if (duitlist.Count > 0)
            {
                return duitlist[0];
            }
            else
            {
                return null;
            }
        }
        public DuitageGomme GetDuitageGo(int MachID, string duit)
        {
            string stm = "Select * from DuitageGomme where MachineID=@id and UPPER(Duitage) like @d";
            List<DuitageGomme> duitlist= db.LoadData<DuitageGomme, dynamic>(stm, new { id = MachID, d = duit.ToUpper() }, connectionStringName);
            if (duitlist.Count > 0)
            {
                return duitlist[0];
            }
            else
            {
                return null;
            }
        }
        public List<Duitages> GetDuitageMachine(Machine machine)
        {
            // string stm = "select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
            // db.LoadData<Duitages, dynamic>(stm, new { id = machine.ID }, connectionStringName);
           using (var conn = new SQLiteConnection(connectionStringName))
           {
               var query = @"select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
 
               var result =  conn.Query<Duitages,Machine,Duitages>(query, (duit, mach) =>
               {
                    duit.Machine= mach;
                   return duit;
               },new {id=machine.ID});
 
               return result.AsList();
           }
        }
        public List<DuitageGomme> GetDuitageMachineGo(Machine machine)
        {
           // string stm = "select * from DuitageGomme as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
             // db.LoadData<DuitageGomme, dynamic>(stm, new { id = machine.ID }, connectionStringName);
            using (var conn = new SQLiteConnection(connectionStringName))
            {
                var query = @"select * from DuitageGomme as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
 
                var result =  conn.Query<DuitageGomme,Machine,DuitageGomme>(query, (duit, mach) =>
                {
                    duit.Machine= mach;
                    return duit;
                },new {id=machine.ID});
 
                return result.AsList();
            }
        }
        public ModelMachine GetTresseCrochetModelMachine(ModelMachine model)
        {
          List<ModelMachine> modellist= db.LoadData<ModelMachine, dynamic>("select * from ModelMachine where UPPER(NomModel) like @model and method=@meth",
                new { model = model.NomModel.ToUpper(),meth=model.method }, connectionStringName);
          if (modellist.Count > 0)
          {
              return modellist[0];
          }
          else
          {
              return null;
          }
        }

        public void AddTresseCrochetModelMachine(ModelMachine model)
        {
            string stm = "Insert into ModelMachine(Name,NomModel,method) values(@name,@model,@method)";
            db.SaveData<dynamic>(stm, new { name = model.Name, model = model.NomModel, method = model.method },
                connectionStringName);
        }
        public void EditMachine(Machine mach)
        {
            string stm = "update mMachine  set Num=@num,DoubleDuitage=@dub,Name=@name,ModelID=@model where ID=@id";
            db.SaveData<dynamic>(stm, new { name = mach.Name ,num=mach.Num,model=mach.Model.ID,dub=mach.DoubleDuitage,id=mach.ID}, connectionStringName);
        }
        public void EditDuitage(Duitages duit)
        {
            string stm = "update Duitages  set MachineID=@MachID,Duitage=@duits,Vitesse=@vit where ID=@id";
            db.SaveData<dynamic>(stm, new { MachID = duit.Machine.ID ,duits=duit.Duitage,vit=duit.Vitesse,id=duit.ID}, connectionStringName);
        }
        public void EditDuitageGo(DuitageGomme duit)
        {
            string stm = "update DuitageGomme  set MachineID=@MachID,Duitage=@duits where ID=@id";
            db.SaveData<dynamic>(stm, new { MachID = duit.Machine.ID ,duits=duit.Duitage,vit=duit.Vitesse,id=duit.ID}, connectionStringName);
        }
        public List<Machine> GetMachines()
        {
            using (var conn = new SQLiteConnection(connectionStringName))
            {
                var query = @"Select * from mMachine as mach,ModelMachine as mMach where mach.ModelID=mMach.ID";
 
                var result =  conn.Query<Machine,ModelMachine,Machine>(query, (mach, mMach) =>
                {
                    mach.Model = mMach;
                    return mach;
                });
 
                return result.AsList();
            }
          //  return  db.LoadData<Machine, dynamic>("Select * from mMachine,ModelMachine where mMachine.ModelID=ModelMachine.ID", null, connectionStringName);
        }
        public void SaveModelMachineChange(ModelMachine model)
        {
            db.SaveData<dynamic>(
                "Update ModelMachine set Name=@name,NomModel=@nom,MaxWidth=@width,NbrBande=@nbr where ID=@id",
                new { name = model.Name,nom=model.NomModel, width = model.MaxWidth, nbr = model.NbrBande, id = model.ID },
                connectionStringName);
            
        }
        public void SaveModelCrochetTresseMachineChange(ModelMachine model)
        {
            db.SaveData<dynamic>(
                "Update ModelMachine set Name=@name,NomModel=@nom,method=@meth where ID=@id",
                new { name = model.Name,nom=model.NomModel, id = model.ID,meth=model.method },
                connectionStringName);
            
        }
        public bool CheckModelDuplicate(ModelMachine model)
        {
          List<ModelMachine> modellist= db.LoadData<ModelMachine, dynamic>("select * from ModelMachine where NbrBande=@nbr and MaxWidth=@width",
                new { nbr = model.NbrBande, width = model.MaxWidth }, connectionStringName);
          if (modellist.Count > 0)
          {
              return true;
          }
          else
          {
              return false;
          }
        }
        public List<Redacteur> GetRedacteurs()
        {

            return db.LoadData<Redacteur, dynamic>("Select * from Redacteur", null, connectionStringName);
        }
        public List<Concepteur> GetConcepteurs()
        {

            return db.LoadData<Concepteur, dynamic>("Select * from Concepteur", null, connectionStringName);
        }
        public void DeleteRedacteur(Redacteur red)
        {
            string stm = "Delete from Redacteur where ID=@id";
            db.SaveData<dynamic>(stm, new { id = red.ID }, connectionStringName);
        }
        public int AddNewEnfilageMatrix(EnfilageMatrix NewEnfMatrix)
        {
            string stm = "Insert into EnfilageMatrix(ID,x,y,valueID,DentFil,EnfID) values(@id,@x,@y,@val,@df,@enfID)";
            int MatID = Convert.ToInt32(db.SaveData(stm,
                new
                {
                    id = NewEnfMatrix.ID, x = NewEnfMatrix.x, y = NewEnfMatrix.y, val = NewEnfMatrix.value.ID,
                    df = NewEnfMatrix.DentFil, enfID = NewEnfMatrix.Enf.ID
                },
                connectionStringName)); 
          
            return MatID;
        }
        public void UpdateEnfilageElement(int  x,int y,int EnfID,Composition content)
        {
            string stm = "Update EnfilageMatrix set valueID=@val where ID=@id and x=@x and y=@y";
            db.SaveData(stm, new { val = content.ID,id=EnfID,x=x,y=y }, connectionStringName);
           
          
        }
        public void DeleteEnfilageElement(int  x,int y,int EnfID)
        {
            string stm = "Delete from EnfilageMatrix where EnfID=@enfid and x=@x and y=@y";

            db.SaveData(stm, new { Enfid = EnfID, x = @x, y = @y }, connectionStringName);
         
        }
        public List<Composition> GetCompositions(int ProdId,int ProdVersion)
        {
            string stm = "Select cmp.ID,cmp.BKBorderComposant,cmp.BKComposant,cmp.NumComposant,cmp.FGComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                         ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId,cmp.ImagePath,cmp.ImageReedPath" +
                         ",cmp.EnfNbrFil" +
                         " from Composition as cmp,Product as pr where cmp.ProdIDId=pr.Id and pr.Id=@id and pr.Version=@vers";
          return  db.LoadData<Composition, dynamic>(stm, new { id = ProdId, vers = ProdVersion }, connectionStringName);
       
        }
        public int GetLatestEnfilageMatrixID()
        {
            string stm = "Select * from EnfilageMatrix order by ID Desc LIMIT 1";
           List<EnfilageMatrix> enflist= db.LoadData<EnfilageMatrix, dynamic>(stm, null, connectionStringName);

           if (enflist.Count > 0)
           {
               return (enflist[0].ID+1);
           }
           else
           {
               return 0;
           }
          
        }
        public List<FicheTechnique> GetFicheTechniquesEch()
        {
        
            string stm2 = "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and pr.Version==0 GROUP by pr.FicheId ORDER by pr.Version";
          List<FicheTechnique> ftlist=  db.LoadData<FicheTechnique,dynamic>(stm2, null, connectionStringName);

          
          if (ftlist.Count > 0)
          {
              
             
              foreach (var fiche in ftlist)
              {
                  string stm = "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                  List<Catalogue> catlist =
                      db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                  if (catlist.Count > 0)
                  {

                      fiche.Catalog = catlist[0];
                  }
                  stm2 = "Select * from Product as pr where pr.FicheId=@id";
                  List<Produit> prlist=  db.LoadData<Produit,dynamic>(stm2, new {id=fiche.ID}, connectionStringName);

                  fiche.Produits = prlist;
                  if (prlist.Count > 0)
                  {
                      foreach (var pr in prlist)
                      {
                          string stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                          List<Composition> complist=  db.LoadData<Composition,dynamic>(stm3, new {id=pr.Id}, connectionStringName);

                          pr.GetComposition = complist;

                          if (complist.Count > 0)
                          {
                              foreach (var compo in complist)
                              {
                                  string stm4 = "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                  List<Composant> composantList=  db.LoadData<Composant,dynamic>(stm4, new {id=compo.ID}, connectionStringName);

                                  if (composantList.Count > 0)
                                  {
                                      compo.GetComposant = composantList[0];
                                  }
                                
                              }
                          }
                      }
              
                  }
              }
              
          }
         


          return ftlist;
        }
         public List<FicheTechnique> GetFicheTechniquesProd()
        {
        
            string stm2 = "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and pr.Version!=0 GROUP by pr.FicheId ORDER by pr.Version";
          List<FicheTechnique> ftlist=  db.LoadData<FicheTechnique,dynamic>(stm2, null, connectionStringName);

          
          if (ftlist.Count > 0)
          {
              
             
              foreach (var fiche in ftlist)
              {
                  string stm = "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                  List<Catalogue> catlist =
                      db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                  if (catlist.Count > 0)
                  {

                      fiche.Catalog = catlist[0];
                  }
                  stm2 = "Select * from Product as pr where pr.FicheId=@id";
                  List<Produit> prlist=  db.LoadData<Produit,dynamic>(stm2, new {id=fiche.ID}, connectionStringName);

                  fiche.Produits = prlist;
                  if (prlist.Count > 0)
                  {
                      foreach (var pr in prlist)
                      {
                          string stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                          List<Composition> complist=  db.LoadData<Composition,dynamic>(stm3, new {id=pr.Id}, connectionStringName);

                          pr.GetComposition = complist;

                          if (complist.Count > 0)
                          {
                              foreach (var compo in complist)
                              {
                                  string stm4 = "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                  List<Composant> composantList=  db.LoadData<Composant,dynamic>(stm4, new {id=compo.ID}, connectionStringName);

                                  if (composantList.Count > 0)
                                  {
                                      compo.GetComposant = composantList[0];
                                  }
                                
                              }
                          }
                      }
              
                  }
              }
              
          }
         


          return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniques()
        {
            string stm = "Select * from FicheTechnique as ft,Catalogue as cat,Product as pr,Composition as compo,Matiere as mat,Concepteur as cp,Verificateur as ver,Client as cl,Duitages as duit,Enfilage as enf,EnfilageMatrix as enfmat,chaine as ch" +
                         " where ft.ID=pr.FicheId and ft.CatalogID=cat.ID and pr.ConcepteurID=cp.ID and pr.VerificateurID=ver.ID and pr.ClientID=cl.ID and pr.DuitageIDID=duit.ID and pr.EnfilageIDID=enf.ID and enfmat.enfID=enf.ID and compo.ProdIDId=pr.Id and enfmat.valueID=compo.ID and enf.GetChaineID=ch.ID";
            string stm2 = "Select * from FicheTechnique as ft";
          List<FicheTechnique> ftlist=  db.LoadData<FicheTechnique,dynamic>(stm2, null, connectionStringName);

          
          if (ftlist.Count > 0)
          {
              
             
              foreach (var fiche in ftlist)
              {
                  stm = "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                  List<Catalogue> catlist =
                      db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                  if (catlist.Count > 0)
                  {

                      fiche.Catalog = catlist[0];
                  }
                  stm2 = "Select * from Product as pr where pr.FicheId=@id";
                  List<Produit> prlist=  db.LoadData<Produit,dynamic>(stm2, new {id=fiche.ID}, connectionStringName);

                  fiche.Produits = prlist;
                  if (prlist.Count > 0)
                  {
                      foreach (var pr in prlist)
                      {
                          string stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                          List<Composition> complist=  db.LoadData<Composition,dynamic>(stm3, new {id=pr.Id}, connectionStringName);

                          pr.GetComposition = complist;

                          if (complist.Count > 0)
                          {
                              foreach (var compo in complist)
                              {
                                  string stm4 = "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                  List<Composant> composantList=  db.LoadData<Composant,dynamic>(stm4, new {id=compo.ID}, connectionStringName);

                                  if (composantList.Count > 0)
                                  {
                                      compo.GetComposant = composantList[0];
                                  }
                                
                              }
                          }
                      }
              
                  }
              }
              
          }
         


          return ftlist;
        }
        
       
    }
}