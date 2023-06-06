using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Transactions;
using BackEnd2.Database;
using BackEnd2.Model;
using Dapper;

namespace BackEnd2.Data
{
    public class SqliteData
    {
        private readonly string PrincipalFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private readonly string connectionStringName;

        private readonly SqliteDataAccess db;

        public SqliteData()
        {
            connectionStringName = "Data Source=FicheTeK.db";
            db = new SqliteDataAccess();
        }
        public void DeleteFicheTechnique(int Ficheid)
        {
            db.SaveData<dynamic>("delete from FicheTechnique where ID=@id", new { id = Ficheid }, connectionStringName);
        }
            public void DeleteFicheTechnique(int Ficheid, int Prodid)
        {
            db.SaveData<dynamic>("delete from FicheTechnique where ID=@id", new { id = Ficheid }, connectionStringName);
            db.SaveData<dynamic>("delete from Product where Id=@id", new { id = Prodid }, connectionStringName);
            db.SaveData<dynamic>("delete from Composition where ProdIDId=@id", new { id = Prodid }, connectionStringName);

            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM FicheTechnique) WHERE name = 'FicheTechnique'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Product) WHERE name = 'Product'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Composition) WHERE name = 'Composition'; ", null, connectionStringName);

        }

        public void DeleteFicheTechnique(int Ficheid, int Prodid, int Enfid)
        {
            db.SaveData<dynamic>("delete from Enfilage where ID=@id", new { id = Enfid }, connectionStringName);
            db.SaveData<dynamic>("delete from EnfilageMatrix where EnfID=@id", new { id = Enfid }, connectionStringName);
            db.SaveData<dynamic>("delete from FicheTechnique where ID=@id", new { id = Ficheid }, connectionStringName);
            db.SaveData<dynamic>("delete from Product where Id=@id", new { id = Prodid }, connectionStringName);
            db.SaveData<dynamic>("delete from Composition where ProdIDId=@id", new { id = Prodid }, connectionStringName);

            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Enfilage) WHERE name = 'Enfilage'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM EnfilageMatrix) WHERE name = 'EnfilageMatrix'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM FicheTechnique) WHERE name = 'FicheTechnique'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Product) WHERE name = 'Product'; ", null, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Composition) WHERE name = 'Composition'; ", null, connectionStringName);

        }

        public List<user> GetUsers()
        {
            return db.LoadData<user, dynamic>("select * from user,usertype where user.type==usertype.ID", null,
                connectionStringName);
        }
        public List<user> GetUserList()
        {
            var users= db.LoadData<user, dynamic>("select * from user", null,
                connectionStringName);
            foreach(user mUser in users)
            {
                mUser.password =new string('*', mUser.password.Length);
            }
            return users;

        }
        public void EditUser(user EditUser)
        {
            string stm = "Update user set username=@name,password=@pass where ID=@id";
            db.SaveData<dynamic>(stm, new { name = EditUser.username, id = EditUser.ID,pass=EditUser.password }, connectionStringName);

        }

        public Machine GetMachine(int num, ModelMachine model)
        {
            var machlist = db.LoadData<Machine, dynamic>(
                "select * from mMachine where ModelID=@model and Num=@num", new { num, model = model.ID },
                connectionStringName);


            if (machlist.Count > 0)
                return machlist[0];
            return null;
        }
        public ModelMachine GetModelMachine(ModelMachine model)
        {
            var machlist = db.LoadData<ModelMachine, dynamic>(
                "select * from ModelMachine where ID=@id", new {id = model.ID },
                connectionStringName);


            if (machlist.Count > 0)
                return machlist[0];
            return null;
        }
        public void AddModelMachine(ModelMachine NewModel)
        {
            var stm = "Insert into ModelMachine(Name,NomModel,NbrBande,MaxWidth,method) values(@name,@NomModel,@NbrBande,@MaxWidth,@method)";
            db.SaveData(stm, new { name = NewModel.Name, NomModel = NewModel.NomModel, NbrBande = NewModel.NbrBande, MaxWidth = NewModel.MaxWidth,method=NewModel.method },
                connectionStringName);
        }

        public void AddNewMachine(Machine mach)
        {
            var stm = "Insert into mMachine(Num,Name,ModelID,DoubleDuitage) values(@num,@name,@model,@dub)";
            db.SaveData(stm, new { num = mach.Num, name = mach.Name, model = mach.Model.ID, dub = mach.DoubleDuitage },
                connectionStringName);
        }

        public void ValidateFicheTechnique(Produit pr)
        {
            db.SaveData<dynamic>("Update Product set Definite=1 where Product.Id=@id", new { pr.Id },
                connectionStringName);
        }

        public List<Reed> GetPeigneList()
        {
            return db.LoadData<Reed, dynamic>("select * from Reed ", null, connectionStringName);
        }

        public void AjouterNouveauPeigne(Reed rd)
        {
            db.SaveData<dynamic>("insert into Reed(Nombre) values(@Numero)", new { Numero = rd.Nombre },
                connectionStringName);
        }

        public int AddNewProduct(Produit NewProd)
        {
            var sqlStm =
                "insert into Product(Ref,Version,FicheId,Definite,Name,IsEnfilage,Redaction,DateCreation) values(@Ref,@Version,@FicheId,@Definite,@Name,@IsEnf,CURRENT_TIMESTAMP,@datecr)";
            NewProd.Id = Convert.ToInt32(db.SaveData(sqlStm,
                new
                {
                    id = NewProd.Id, NewProd.Ref, NewProd.Version, NewProd.Definite, NewProd.FicheId, NewProd.Name,
                    IsEnf = NewProd.IsEnfilage, datecr = NewProd.DateCreation
                }, connectionStringName));
            return NewProd.Id;
        }

        public int AddNewProductVersion(Produit NewProd)
        {
            var sqlStm =
                "insert into Product(Ref,Version,NumArticle,DateCreation,Dent,Largeur,FicheId,Definite,Name,IsEnfilage,Redaction,EnfDent) values(@Ref,@Version,@numAr,@datecr,@dent,@larg,@FicheId,@Definite,@Name,@IsEnf,CURRENT_TIMESTAMP,@EnfDent)";
            NewProd.Id = Convert.ToInt32(db.SaveData(sqlStm, new
            {
                id = NewProd.Id, NewProd.Ref, NewProd.Version, Definite = 0,
                datecr = NewProd.DateCreation, dent = NewProd.Dent, larg = NewProd.Largeur, numAr = NewProd.NumArticle,
                NewProd.FicheId, NewProd.Name, IsEnf = NewProd.IsEnfilage,NewProd.EnfDent
            }, connectionStringName));
            return NewProd.Id;
        }

        public void UpdateProdVersion(Produit NewProd)
        {
            var stm = "Update Product set Version=@ver where Id=@id";
            db.SaveData(stm, new { ver = NewProd.Version, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdImage(Produit NewProd)
        {
            var stm = "Update Product set image=@img where Id=@id";
            db.SaveData(stm, new { img = NewProd.image, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdDuitage(Produit NewProd)
        {
            var stm = "Update Product set DuitageIDID=@duit where Id=@id";
            db.SaveData(stm, new { duit = NewProd.DuitageID.ID, id = NewProd.Id }, connectionStringName);
        }

        public void ResetFTModelProp(Produit NewProd)
        {
            var stm = "Update Product set DuitageIDID=null,DuitageGommeID=null,Peigne=null where Id=@id";
            db.SaveData(stm, new {  id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdDent(Produit NewProd)
        {
            var stm = "Update Product set Dent=@dent where Id=@id";
            db.SaveData(stm, new { dent = NewProd.Dent, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdDuitageGomme(Produit NewProd)
        {
            var stm = "Update Product set DuitageGommeID=@duit where Id=@id";
            db.SaveData(stm, new { duit = NewProd.DuitageGomme.ID, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdNumArticle(Produit NewProd)
        {
            var stm = "Update Product set NumArticle=@num where Id=@id";
            db.SaveData(stm, new { num = NewProd.NumArticle, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdRef(Produit NewProd)
        {
            var stm = "Update Product set Ref=@refe where Id=@id";
            db.SaveData(stm, new { refe = NewProd.Ref, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateEnfDent(Produit NewProd)
        {
            var stm = "Update Product set EnfDent=@enfDent where Id=@id";
            db.SaveData(stm, new { enfDent = NewProd.EnfDent, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdName(Produit NewProd)
        {
            var stm = "Update Product set Name=@name where Id=@id";
            db.SaveData(stm, new { name = NewProd.Name, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdConcepteur(Produit NewProd)
        {
            var stm = "Update Product set ConcepteurID=@concept where Id=@id";
            db.SaveData(stm, new { concept = NewProd.Concepteur.ID, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdVerificateur(Produit NewProd)
        {
            var stm = "Update Product set VerificateurID=@ver where Id=@id";
            db.SaveData(stm, new { ver = NewProd.Verificateur.ID, id = NewProd.Id }, connectionStringName);
        }

        public int AddProdCompo(Composition compo)
        {
            var stm =
                "Insert into Composition(NumComposant,DebutFil,Intermittent,Num" +
                ",NbrFil,Torsion,Enfilage,Emb,Poids,Observation,EnfNbrFil,ProdIDId) values " +
                "(@numComp,@df,@inter,@num,@nbrfil,@tor,@enf,@emb,@poids,@obs,@EnfNbr,@prodid)";
            var NewID = Convert.ToInt32(db.SaveData(stm, new
            {
                numComp = compo.NumComposant,
                 df = compo.DebutFil, inter = compo.Intermittent, num = compo.Num,
                nbrfil = compo.NbrFil, tor = compo.Torsion, enf = compo.Enfilage, emb = compo.Emb, poids = compo.Poids,
                obs = compo.Observation,
                EnfNbr = compo.EnfNbrFil, prodid = compo.ProdID.Id
            }, connectionStringName));
            return NewID;
        }

      

      

        public void UpdateProdRedacteur(Produit NewProd)
        {
            var stm = "Update Product set Redacteur=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.RedacteurObj.ID, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdRedaction(Produit NewProd)
        {
            var stm = "Update Product set Redaction=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.Redaction, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdDateCreation(Produit NewProd)
        {
            var stm = "Update Product set DateCreation=@red where Id=@id";
            db.SaveData(stm, new { red = NewProd.DateCreation, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateChColComp(ChColComp chCol)
        {
            var stm = "Update ChColComp set CompsantID=@compid where ChaineID=@chid and ColNum=@colNum";

            db.SaveData(stm,
                new
                {
                    compid = chCol.ComposantID,
                    chid = chCol.ChaineID,
                    colNum = chCol.ColNum
                }, connectionStringName);
        }

        public void AddChColComp(ChColComp chCol)
        {
            var stm = "Insert into ChColComp(ChaineID,ColNum,ComposantID) values(@chid,@colnum,@compid)";

            db.SaveData(stm,
                new
                {
                    compid = chCol.Comp.ID,
                    chid = chCol.ChaineID,
                    colnum = chCol.ColNum
                }, connectionStringName);
        }

        public List<ChColComp> GetChColComps(int ChID)
        {
            var stm = "Select * from ChColComp where ChaineID=@ChID";
            var chcollist = db.LoadData<ChColComp, dynamic>(stm, new
            {
                ChID
            }, connectionStringName);

            return chcollist;
        }

        public ChColComp GetChColComp(int ChID, int ColNum)
        {
            var stm = "Select * from ChColComp where ChaineID=@ChID and ColNum=@ColNum";
            var chcollist = db.LoadData<ChColComp, dynamic>(stm, new
            {
                ChID,
                ColNum
            }, connectionStringName);
            if (chcollist.Count > 0)
                return chcollist[0];
            return null;
        }

        public List<Composant> GetComposants()
        {
            var stm = "Select * from Composant";
            return db.LoadData<Composant, dynamic>(stm, null, connectionStringName);
        }

        public void UpdateProdMiseAJour(Produit NewProd)
        {
            var stm = "Update Product set MiseAJour=@mise where Id=@id";
            db.SaveData(stm, new { mise = NewProd.MiseAJour, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdName2(Produit NewProd)
        {
            var stm = "Update Product set Name2=@name where Id=@id";
            db.SaveData(stm, new { name = NewProd.Name2, id = NewProd.Id }, connectionStringName);
        }
        public void RemoveProdName2(Produit NewProd)
        {
            var stm = "Update Product set Name2=null where Id=@id";
            db.SaveData(stm, new {  id = NewProd.Id }, connectionStringName);
        }
        public bool CheckVersionUnique(Produit NewProd)
        {
            var stm =
                "select * from Product as pr,FicheTechnique as ft where pr.FicheId=ft.ID and Version=@ver and ft.ID=@id";
            var prodlist = db.LoadData<Produit, dynamic>(stm, new { ver = NewProd.Version, id = NewProd.FicheId },
                connectionStringName);
            if (prodlist.Count > 0)
                return false;
            return true;
        }
        public void ChangeProductFicheID(Produit prod)
        {
            var stm = "update Product set FicheId=@FicheId where Id=@ProdID";
            db.SaveData<dynamic>(stm, new { prod.FicheId, ProdID = prod.Id },connectionStringName);
        }
        public bool CheckNArticleUnique(int NumArticle, int ProdID)
        {
            var stm = "select * from Product where NumArticle=@num and Id!=@id";
            var prodlist = db.LoadData<Produit, dynamic>(stm, new { num = NumArticle, id = ProdID },
                connectionStringName);
            if (prodlist.Count > 0)
                return false;
            return true;
        }
        public bool CheckCouleurUnique(int Num, string name)
        {
            var stm = "select * from Color where Nbr=@num";
            var prodlist = db.LoadData<Couleur, dynamic>(stm, new { num = Num },
                connectionStringName);

            if (prodlist.Count > 0)
                return false;
            else
            {
                stm = "select * from Color where Name=@Name";
                prodlist = db.LoadData<Couleur, dynamic>(stm, new { Name = name },
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

        }
        public bool CheckCouleurUniqueEdit(int id,int Num, string name)
        {
            var stm = "select * from Color where Nbr=@num and ID!=@id";
            var prodlist = db.LoadData<Couleur, dynamic>(stm, new { Nbr = Num, id=id},
                connectionStringName);

            if (prodlist.Count > 0)
                return false;
            else
            {
                 stm = "select * from Color where Name=@Name and ID!=@id";
                 prodlist = db.LoadData<Couleur, dynamic>(stm, new { Name = name, id = id },
                    connectionStringName);
                if(prodlist.Count>0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
            
        }
        public void UpdateProdClient(Produit NewProd)
        {
            var stm = "Update Product set ClientID=@cl where Id=@id";
            db.SaveData(stm, new { id = NewProd.Id, cl = NewProd.Client.ID }, connectionStringName);
        }
        public void RemoveProdClient(Produit NewProd)
        {
            var stm = "Update Product set ClientID=null where Id=@id";
            db.SaveData(stm, new { id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdLargeur(Produit NewProd)
        {
            var stm = "Update Product set Largeur=@larg where Id=@id";
            db.SaveData(stm, new { larg = NewProd.Largeur, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdEpaiseur(Produit NewProd)
        {
            var stm = "Update Product set Epaisseur=@ep where Id=@id";
            db.SaveData(stm, new { ep = NewProd.Epaisseur, id = NewProd.Id }, connectionStringName);
        }

        public void UpdateProdIsEnfilage(Produit NewProd)
        {
            var stm = "Update Product set IsEnfilage=@IsEnf where Id=@id";
            db.SaveData(stm, new { IsEnf = NewProd.IsEnfilage, id = NewProd.Id }, connectionStringName);
        }
        

        public void UpdateProdEnfilage(Produit NewProd)
        {
            var stm = "Update Product set EnfilageIDID=@enf where Id=@id";
            db.SaveData(stm, new { enf = NewProd.EnfilageID.ID, id = NewProd.Id }, connectionStringName);
        }
        public void UpdateProdRemoveEnfilage(Produit NewProd)
        {
            var stm = "Update Product set EnfilageIDID=null where Id=@id";
            db.SaveData(stm, new {  id = NewProd.Id }, connectionStringName);
        }
        public int AddNewProductWithEnfilage(Produit NewProd)
        {
            var sqlStm =
                "insert into Product(Id,Ref,Version,FicheId,Definite,Name,IsEnfilage,EnfilageIDID,Redaction,DateCreation) values(@id,@Ref,@Version,@FicheId,@Definite,@Name,@IsEnf,@EnfID,CURRENT_TIMESTAMP,@datecr)";
            NewProd.Id = Convert.ToInt32(db.SaveData(sqlStm,
                new
                {
                    id = NewProd.Id, NewProd.Ref, NewProd.Version, NewProd.Definite, NewProd.FicheId, NewProd.Name,
                    IsEnf = NewProd.IsEnfilage, EnfID = NewProd.EnfilageID.ID, datecr = NewProd.DateCreation
                }, connectionStringName));
            return NewProd.Id;
        }

        public void UpdateFicheTechniqueCategorie(int FtID, Catalogue cat)
        {
            var stm = "Update FicheTechnique set CatalogID=@catID where ID=@id";
            db.SaveData<dynamic>(stm, new { catID = cat.ID, id = FtID }, connectionStringName);
        }

        public void DeletePeigne(Reed rd)
        {
            db.SaveData<dynamic>("Delete from  Reed  where ID=@id", new { id = rd.ID }, connectionStringName);
        }
        public void RemoveProdCompo(Composition cp)
        {
            db.SaveData<dynamic>("Delete from  Composition  where ID=@id", new { id = cp.ID }, connectionStringName);
        }
        public void RemoveEnfilageChaine(Enfilage enf)
        {
            db.SaveData<dynamic>("Update Enfilage set GetChaineID=null  where ID=@id", new { id = enf.ID }, connectionStringName);
        }
        public void ModifierNouveauPeigne(Reed rd)
        {
            db.SaveData<dynamic>("update  Reed set Nombre=@num where ID=@id", new { Numero = rd.Nombre, id = rd.ID },
                connectionStringName);
        }

        public void RemoveEnfilage(Enfilage mEnfilage)
        {
            var stm = "Delete from Enfilage where ID=@id";
            db.SaveData(stm, new { id = mEnfilage.ID }, connectionStringName);
            db.SaveData<dynamic>("update sqlite_sequence SET seq = (SELECT MAX(id) FROM Enfilage) WHERE name = 'Enfilage'; ", null, connectionStringName);
        }

        public int AddNewEnfilage(Enfilage NewEnfliage)
        {
            return Convert.ToInt32(db.SaveData<dynamic>(
                "insert into Enfilage(TrXposition,TrYposition,Column,Row,NbrDent) values(@Trx,@Try,@col,@row,@Dent)",
                new
                {
                    Trx = NewEnfliage.TrXposition,
                    Try = NewEnfliage.TrYposition,
                    col = NewEnfliage.Column,
                    row = NewEnfliage.Row,
                    Dent = NewEnfliage.NbrDent
                }, connectionStringName));
        }
        public int AddNewEnfilageWithChaine(Enfilage NewEnfliage)
        {
            return Convert.ToInt32(db.SaveData<dynamic>(
                "insert into Enfilage(TrXposition,TrYposition,Column,Row,NbrDent,GetChaineID) values(@Trx,@Try,@col,@row,@Dent,@ch)",
                new
                {
                    Trx = NewEnfliage.TrXposition,
                    Try = NewEnfliage.TrYposition,
                    col = NewEnfliage.Column,
                    row = NewEnfliage.Row,
                    Dent = NewEnfliage.NbrDent,
                    ch = NewEnfliage.GetChaine.ID
                }, connectionStringName));
        }

        public int GetLastProductID()
        {
            var obj = db.LoadData<Produit, dynamic>("Select * from Product", null, connectionStringName);
            if (obj != null && obj.Count > 0)
            {
                var prlist = db.LoadData<int, dynamic>("Select max(Id) from Product order by Id desc", null,
                    connectionStringName);
                if (prlist != null && prlist.Count > 0)
                    return prlist[0];
                return 0;
            }

            return 0;
        }

        public FicheTechnique AddNewFicheTechnique(FicheTechnique NewFiche)
        {
            var SqlStm = "Insert into FicheTechnique(ModelFiche,IsArchive) values(@model,@Arch)";
            var id = Convert.ToInt32(db.SaveData<dynamic>(SqlStm,
                new { model = NewFiche.ModelFiche, Arch = NewFiche.IsArchive }, connectionStringName));

            var ft = db.LoadData<FicheTechnique, dynamic>("Select * from FicheTechnique where ID=@id", new { id },
                connectionStringName);
            return ft[0];
        }
        public void ChangeModelFicheTechnique(FicheTechnique NewFiche)
        {
            var SqlStm = "update FicheTechnique set ModelFiche=@num where ID=@ID";
            db.SaveData<dynamic>(SqlStm,
                new { num = NewFiche.ModelFiche, NewFiche.ID }, connectionStringName);

        }

        public void UpdateProdPeigne(Produit pr)
        {
            db.SaveData<dynamic>("Update Product set Peigne=@reed where Product.Id=@id",
                new { reed = pr.PeigneObj.ID, id = pr.Id }, connectionStringName);
        }

        public void UpdateEnfilageChaine(Enfilage enf)
        {
            var stm = "Update Enfilage set GetChaineID=@chid where ID=@id";
            db.SaveData(stm, new { id = enf.ID, chid = enf.GetChaine.ID }, connectionStringName);
        }

        public List<chaine> GetChaines()
        {
            var SqlStm = "Select * from Chaine as ch";

            var chlist = db.LoadData<chaine, dynamic>(SqlStm, null, connectionStringName);
            foreach (var ch in chlist)
            {
                SqlStm = "Select * from ChaineMatrix as chmat where ChaineID=@id";
                ch.ChMatrix = db.LoadData<ChaineMatrix, dynamic>(SqlStm, new { id = ch.ID }, connectionStringName);
                using (var conn = new SQLiteConnection(connectionStringName))
                {
                    SqlStm =
                        "Select * from ChColComp as chc,Composant as cmp where  chc.ComposantID=cmp.ID and chc.ChaineID=@chid";

                    var result = conn.Query<ChColComp, Composant, ChColComp>(SqlStm, (chc, cmp) =>
                    {
                        chc.Comp = cmp;
                        return chc;
                    }, new { chid = ch.ID });

                    ch.ChaineCompos = result.AsList();
                }
            }

            return chlist;
        }

        public List<Machine> GetCrochtageMachines()
        {
            var sqlStm = "Select * from mMachine where DoubleDuitage=1";
            return db.LoadData<Machine, dynamic>(sqlStm, null, connectionStringName);
        }

        public void GetFullCompositions(Produit pr,SQLiteConnection conn)
        {
            var stm1 = "Select * from (select * from Composition  where ProdIDId=@id) as comp  " +
                      "left join Matiere as mat on mat.ID=comp.GetMatiereID " +
                      "left join Composant as cp on cp.ID=comp.GetComposantID " +
                      " left join Titrage as tit on tit.ID=mat.TitrageID " +
                      " left join TypeMatiere as mattyp on mattyp.ID=tit.TypeMatiereID  " +
                      " left join Color as col on col.ID=mat.GetCouleurID ";
            
                var complist = conn.Query<Composition, Matiere, Composant, Titrage, TypeMatiere, Couleur, Composition>(stm1,
                    (comp, mat, cp, tit, mattyp, col) =>
                    {
                        comp.GetMatiere = mat;
                        comp.GetComposant = cp;
                        if (comp.GetMatiere != null)
                        {
                            comp.GetMatiere.Titrage = tit;
                            comp.GetMatiere.GetCouleur = col;
                            if (comp.GetMatiere.Titrage != null)
                                comp.GetMatiere.Titrage.TypeMatiere = mattyp;

                        }



                        return comp;
                    }, new { id = pr.Id });
                pr.GetComposition = complist.ToList();
        }

        public List<Produit> GetProductByCreationDate(DateTime Startdate,DateTime Enddate)
        {
            string stm = "Select * from Product where DateCreation BETWEEN @StartDate AND @EndDate";
           return db.LoadData<Produit,dynamic>(stm, new
            {
                StartDate = Startdate,
                EndDate = Enddate
            }, connectionStringName);
        }

        public List<Produit> GetProductsByFicheTechnique(int FicheID)
        {
            string stm = "Select * from Product where FicheId=@id";
            return db.LoadData<Produit,dynamic>(stm, new
            {
                id = FicheID
            }, connectionStringName);
            
        }
        public List<Produit> GetProductByUpdateDate(DateTime Startdate,DateTime Enddate)
        {
            string stm = "Select * from Product where MiseAJour BETWEEN @StartDate AND @EndDate";
            return db.LoadData<Produit,dynamic>(stm, new
            {
                StartDate = Startdate,
                EndDate = Enddate
            }, connectionStringName);
        }
        public void GetUpperDataSheetPart(Produit pr, SQLiteConnection conn)
        {
           var stm1 = "Select * from (select * from Product where Id=@id) as pr" +
                   " left join Client as cl on cl.ID=pr.ClientID" +
                   " left join Duitages as duit on duit.ID=pr.DuitageIDID" +
                   " left join mMachine as mach on mach.ID=duit.MachineID" +
                   " left join DuitageGomme as duitgo on duitgo.ID=pr.DuitageGommeID" +
                   " left join Concepteur as concept on concept.ID=pr.ConcepteurID" +
                   " left join Reed on Reed.ID=pr.Peigne" +
                   " left join Redacteur as red on red.ID=pr.Redacteur" +
                   " left join Verificateur as ver on ver.ID=pr.VerificateurID";
          
                conn.Query<Produit>(stm1, new[]
                {
                    typeof(Produit),
                    typeof(Client),
                    typeof(Duitages),
                    typeof(Machine),
                    typeof(DuitageGomme),
                    typeof(Concepteur),
                    typeof(Reed),
                    typeof(Redacteur),
                    typeof(Verificateur)
                }, obj =>
                {
                    Produit prd = obj[0] as Produit;
                    Client cl = obj[1] as Client;
                    Duitages duit = obj[2] as Duitages;
                    Machine mach = obj[3] as Machine;
                    DuitageGomme duitgo = obj[4] as DuitageGomme;
                    Concepteur concept = obj[5] as Concepteur;
                    Reed rd = obj[6] as Reed;
                    Redacteur red = obj[7] as Redacteur;
                    Verificateur ver = obj[8] as Verificateur;
                    pr.Client = cl;
                    pr.DuitageID = duit;
                    pr.DuitageGomme = duitgo;
                    if (pr.DuitageID != null)
                        pr.DuitageID.Machine = mach;

                    pr.Concepteur = concept;
                    pr.PeigneObj = rd;
                    pr.RedacteurObj = red;
                    pr.Verificateur = ver;
                    return prd;
                }, new { id = pr.Id });

            
        }
        public void GetFullEnfilage(Produit pr)
        {
            SQLiteConnection conn = new SQLiteConnection(connectionStringName);
            if (pr.IsEnfilage == 1)
            {

                var stm =
                     "Select enf.ID,enf.GetChaineID,enf.TrXposition,enf.TrYposition,enf.Column,enf.Row,enf.NbrDent from Enfilage as enf,Product as pr where pr.EnfilageIDID=enf.ID and pr.Id=@id";
                var enflist =
                    db.LoadData<Enfilage, dynamic>(stm, new { id = pr.Id }, connectionStringName);
                if (enflist.Count > 0)
                {
                    pr.EnfilageID = enflist[0];
                    stm = "Select * from Enfilage as enf,Chaine as ch where ch.ID=enf.GetChaineID and enf.ID=@id";
                    var chlist =
                        db.LoadData<chaine, dynamic>(stm, new { id = pr.EnfilageID.ID }, connectionStringName);

                    if (chlist.Count > 0) pr.EnfilageID.GetChaine = chlist[0];
                    if (pr.EnfilageID.GetChaine != null)
                    {

                        stm =
                            "Select * from ChColComp as ch,Composant as cmp where ch.ComposantID=cmp.ID and ch.ChaineID=@chid";


                        var result = conn.Query<ChColComp, Composant, ChColComp>(stm, (ch, cmp) =>
                        {
                            ch.Comp = cmp;
                            return ch;
                        }, new { chid = pr.EnfilageID.GetChaine.ID });

                        pr.EnfilageID.GetChaine.ChaineCompos = result.AsList();


                        stm =
                            "Select * from ChaineMatrix as chmat,Chaine as ch where chmat.ChaineID=ch.ID and ch.ID=@id";
                        pr.EnfilageID.GetChaine.ChMatrix =
                            db.LoadData<ChaineMatrix, dynamic>(stm, new { id = pr.EnfilageID.GetChaine.ID },
                                connectionStringName);
                    }



                    stm = "Select " +
                          "enf.ID,enfmat.ID,enfmat.x,enfmat.y,enfmat.valueID,enfmat.DentFil,enfmat.EnfID" +
                          ",cmp.ID,cmp.NumComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                          ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId" +
                          ",cmp.EnfNbrFil" +
                          " from Enfilage as enf,Composition as cmp,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enfmat.valueID=cmp.ID and enf.ID=@id";
                    var result2 = conn.Query<EnfilageMatrix, Composition, EnfilageMatrix>(stm, (enfmat, cmp) =>
                    {
                        enfmat.value = cmp;
                        return enfmat;
                    }, new { id = pr.EnfilageID.ID });

                    pr.EnfilageID.GetMatrix = result2.AsList();

                    stm =
                        "Select * from Enfilage as enf,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enf.ID=@id and DentFil=2";
                    var EmptyCells = db.LoadData<EnfilageMatrix, dynamic>(stm, new { id = pr.EnfilageID.ID },
                        connectionStringName);
                    pr.EnfilageID.GetMatrix.AddRange(EmptyCells);


                }
            }
        }
        public void GetFullEnfilage(Produit pr,SQLiteConnection conn)
        {
            if (pr.IsEnfilage == 1)
            {

                var stm =
                     "Select enf.ID,enf.GetChaineID,enf.TrXposition,enf.TrYposition,enf.Column,enf.Row,enf.NbrDent from Enfilage as enf,Product as pr where pr.EnfilageIDID=enf.ID and pr.Id=@id";
                var enflist =
                    db.LoadData<Enfilage, dynamic>(stm, new { id = pr.Id }, connectionStringName);
                if (enflist.Count > 0)
                {
                    pr.EnfilageID = enflist[0];
                    stm = "Select * from Enfilage as enf,Chaine as ch where ch.ID=enf.GetChaineID and enf.ID=@id";
                    var chlist =
                        db.LoadData<chaine, dynamic>(stm, new { id = pr.EnfilageID.ID }, connectionStringName);

                    if (chlist.Count > 0) pr.EnfilageID.GetChaine = chlist[0];
                    if (pr.EnfilageID.GetChaine != null)
                    {
                       
                            stm =
                                "Select * from ChColComp as ch,Composant as cmp where ch.ComposantID=cmp.ID and ch.ChaineID=@chid";


                            var result = conn.Query<ChColComp, Composant, ChColComp>(stm, (ch, cmp) =>
                            {
                                ch.Comp = cmp;
                                return ch;
                            }, new { chid = pr.EnfilageID.GetChaine.ID });

                            pr.EnfilageID.GetChaine.ChaineCompos = result.AsList();
                        

                        stm =
                            "Select * from ChaineMatrix as chmat,Chaine as ch where chmat.ChaineID=ch.ID and ch.ID=@id";
                        pr.EnfilageID.GetChaine.ChMatrix =
                            db.LoadData<ChaineMatrix, dynamic>(stm, new { id = pr.EnfilageID.GetChaine.ID },
                                connectionStringName);
                    }

                  

                        stm = "Select " +
                              "enf.ID,enfmat.ID,enfmat.x,enfmat.y,enfmat.valueID,enfmat.DentFil,enfmat.EnfID" +
                              ",cmp.ID,cmp.NumComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                              ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId" +
                              ",cmp.EnfNbrFil" +
                              " from Enfilage as enf,Composition as cmp,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enfmat.valueID=cmp.ID and enf.ID=@id";
                        var result2 = conn.Query<EnfilageMatrix, Composition, EnfilageMatrix>(stm, (enfmat, cmp) =>
                        {
                            enfmat.value = cmp;
                            return enfmat;
                        }, new { id = pr.EnfilageID.ID });

                        pr.EnfilageID.GetMatrix = result2.AsList();
                        
                        stm =
                            "Select * from Enfilage as enf,EnfilageMatrix as enfmat where enfmat.EnfID=enf.ID and enf.ID=@id and DentFil=2";
                        var EmptyCells = db.LoadData<EnfilageMatrix, dynamic>(stm, new { id = pr.EnfilageID.ID },
                            connectionStringName);
                        pr.EnfilageID.GetMatrix.AddRange(EmptyCells);
                            
                    
                }
            }
        }

        public Produit GetFullProduct(Produit pr)
        {
            
                using (var conn =new SQLiteConnection(connectionStringName))
                {
                    GetFullCompositions(pr,conn);

                    GetUpperDataSheetPart(pr,conn);

                    GetFullEnfilage(pr,conn);
            }
                   
                


            return pr;

        }


        public void AssignOrderToFicheTechnique(int CatID)
        {
            var stm =
                "Select max(pr.Version) as Version,pr.FicheId,pr.Name,pr.Largeur,pr.Epaisseur from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and ft.CatalogID=@catid group by pr.FicheId ORDER by Largeur,Epaisseur,Name";
            var prlist = db.LoadData<Produit, dynamic>(stm, new { catid = CatID }, connectionStringName);
            for (var i = 0; i < prlist.Count; i++)
            {
                var ordre = i + 1;
                var stm2 = "Update FicheTechnique set Ordre=@ordre where ID=@fichId";
                db.SaveData(stm2, new { FichId = prlist[i].FicheId, ordre }, connectionStringName);
            }
        }

        public int AssignOrderToFicheTechnique(int FicheID, int CatID)
        {
            var ord = 1;
            var stm =
                "Select max(pr.Version) as Version,pr.FicheId,pr.Name,pr.Largeur,pr.Epaisseur from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and ft.CatalogID=@catid group by pr.FicheId ORDER by Largeur,Epaisseur,Name";
            var prlist = db.LoadData<Produit, dynamic>(stm, new { catid = CatID }, connectionStringName);
            for (var i = 0; i < prlist.Count; i++)
            {
                var ordre = i + 1;
                var stm2 = "Update FicheTechnique set Ordre=@ordre where ID=@fichId";
                db.SaveData(stm2, new { FichId = prlist[i].FicheId, ordre }, connectionStringName);
                if (prlist[i].FicheId == FicheID) ord = ordre;
            }

            return ord;
        }

        public List<Catalogue> GetCategorieChildren(int CatID)
        {
            var stm = "Select * from Catalogue where parent=@catID";
            var listcat = db.LoadData<Catalogue, dynamic>(stm,
                new
                {
                    catID = CatID
                }, connectionStringName);


            return listcat;
        }

        public List<Catalogue> GetRootCategories()
        {
            var stm = "Select * from Catalogue where parent=-1";
            var listcat = db.LoadData<Catalogue, dynamic>(stm, null, connectionStringName);


            return listcat;
        }

        public List<Catalogue> GetCategoriesWithoutChildren()
        {
            var stm = "Select * from Catalogue";
            var PureCatList = new List<Catalogue>();
            var listcat = db.LoadData<Catalogue, dynamic>(stm, null, connectionStringName);
            foreach (var cat in listcat)
            {
                var HaveChild = listcat.FirstOrDefault(cata => cata.parent == cat.ID);
                if (HaveChild == null) PureCatList.Add(cat);
            }

            return PureCatList;
        }
        public int AddNewReptition(Repitition rep)
        {
            return Convert.ToInt32( db.SaveData<dynamic>("Insert into Repitition(enfilageID,x,y) values(@enfID,@x,@y)", 
                new {  enfID=rep.enfilageID, x=rep.x,y=rep.y },
                connectionStringName));
        }
        public int UpdateReptitionPosition(Repitition rep)
        {
            return Convert.ToInt32( db.SaveData<dynamic>("update Repitition set x=@x,y=@y,value=@val where id=@id", 
                new {  id=rep.id, x=rep.x,y=rep.y,val=rep.value },
                connectionStringName));
        }
        public List<Repitition> getRepition(int  enfilageID)
        {
            return db.LoadData<Repitition, dynamic>("Select * from Repitition where enfilageID=@enfID",
                new
                {
                    enfID=enfilageID
                }, connectionStringName);
        }
        
        public int AddNewReport(MonthlyReport rep)
        {
            return Convert.ToInt32( db.SaveData<dynamic>("Insert into MonthlyReport(year,month) values(@year,@month)", 
                new {  year=rep.year, month=rep.month },
                connectionStringName));
        }
        public List<ReportProduct> GetReportArticles(int repID)
        {
            return db.LoadData<ReportProduct, dynamic>("Select * from monthlyReportArticle where reportID=@repID",
                new
                {
                    repID=repID
                }, connectionStringName);
        }
        public ReportProduct GetReportArticle(int repID,string RefProd)
        {
            var repProd= db.LoadData<ReportProduct, dynamic>("Select * from monthlyReportArticle where reportID=@repID and ref=@RefProd",
                new
                {
                    repID=repID,
                    RefProd
                    
                }, connectionStringName);
            if (repProd != null && repProd.Count > 0)
            {
                return repProd[0];
                
            }
            else
            {
                return null;
            }
        }
        public List<ReportProduct> GetReportArticles()
        {
            return db.LoadData<ReportProduct, dynamic>("Select * from monthlyReportArticle ",
                null, connectionStringName);
        }
        public List<MonthlyReport> GetReports()
        {
            return db.LoadData<MonthlyReport, dynamic>("Select * from monthlyReport",
                null, connectionStringName);
        }
        public List<MonthlyReport> GetReports(int year)
        {
            return db.LoadData<MonthlyReport, dynamic>("Select * from monthlyReport where year=@year",
                new {year}, connectionStringName);
        }
        public int AddNewReportArticles(ReportProduct repArticle)
        {
            return Convert.ToInt32( db.SaveData<dynamic>(
                "Insert into MonthlyReportArticle(DateProd,ref,designation,version,creation,miseajour,nonconforme,remarque,reportID,categorie) " +
                                                         "values(@date,@refs,@des,@ver,@creation,@update,@conforme,@req,@repid,@cat)", 
                new {  date=repArticle.DateProd,refs=repArticle.Ref, 
                    des=repArticle.Designation,ver=repArticle.Version,creation=repArticle.Creation,
                    update=repArticle.miseajour,conforme=repArticle.nonConforme,req=repArticle.Remarque,repid=repArticle.repID
                    ,cat=repArticle.categorie
                },
                connectionStringName));
        }
        
        public int UpdateMonthlyReportArticle(ReportProduct repArticle)
        {
            return Convert.ToInt32( db.SaveData<dynamic>(
                "update  MonthlyReportArticle set  " +
                "dateprod=@date,ref=@refs,designation=@des,version=@ver,creation=@creation,miseajour=@update,nonconforme=@conforme,remarque=@req,categorie=@cat where id=@id", 
                new {  date=repArticle.DateProd,refs=repArticle.Ref, 
                    des=repArticle.Designation,ver=repArticle.Version,creation=repArticle.Creation,
                    update=repArticle.miseajour,conforme=repArticle.nonConforme,req=repArticle.Remarque
                    ,id=repArticle.id,cat=repArticle.categorie
                    
                },
                connectionStringName));
        }
        public void UpdateChaine(chaine ch)
        {
           
                var stm2 = "Update Chaine set Nom=@nom,Colonne=@col,Ligne=@row where ID=@id";
                db.SaveData(stm2, new { nom=ch.Nom,col=ch.Colonne,row=ch.Ligne,id=ch.ID }, connectionStringName);
          
        }
        public int AddNewChaine(chaine ch)
        {
           return Convert.ToInt32( db.SaveData<dynamic>("Insert into Chaine(Nom,Colonne,Ligne) values(@Nom,@Colonne,@Ligne)", 
                new { Nom = ch.Nom, Colonne=ch.Colonne, Ligne=ch.Ligne },
                connectionStringName));
        }
        public int AddNewChaineElement(ChaineMatrix chmat)
        {
            return Convert.ToInt32(db.SaveData<dynamic>("Insert into ChaineMatrix(x,y,ChaineID) values(@x,@y,@ChaineID)",
                 new { x = chmat.x, y = chmat.y, ChaineID = chmat.Chaine.ID },
                 connectionStringName));
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
            var redlist = db.LoadData<Redacteur, dynamic>("Select * from Redacteur where Name=@name",
                new { name = nameRed }, connectionStringName);
            if (redlist.Count > 0)
                return redlist[0];
            return null;
        }

        public List<ModelMachine> GetModelMachines()
        {
            return db.LoadData<ModelMachine, dynamic>("Select * from ModelMachine", null, connectionStringName);
        }

        public void AddNewDuitage(Duitages duita)
        {
            var stm = "Insert into Duitages(MachineID,Duitage,Vitesse) values(@mach,@duit,@vit)";
            db.SaveData<dynamic>(stm, new { mach = duita.Machine.ID, duit = duita.Duitage, vit = duita.Vitesse },
                connectionStringName);
        }

        public void AddNewDuitageGo(DuitageGomme duita)
        {
            var stm = "Insert into DuitageGomme(MachineID,Duitage) values(@mach,@duit)";
            db.SaveData<dynamic>(stm, new { mach = duita.Machine.ID, duit = duita.Duitage }, connectionStringName);
        }

        public Duitages GetDuitage(int MachID, double duit)
        {
            var stm = "Select * from Duitages where MachineID=@id and Duitage=@d";
            var duitlist = db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit }, connectionStringName);
            if (duitlist.Count > 0)
                return duitlist[0];
            return null;
        }

        public Duitages GetDuitageEdit(int MachID, double duit, double Vitesse)
        {
            var stm = "Select * from Duitages where MachineID=@id and Duitage=@d and Vitesse=@vit";
            var duitlist = db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit, vit = Vitesse },
                connectionStringName);
            if (duitlist.Count > 0)
                return duitlist[0];
            return null;
        }

        public Duitages GetDuitageGoEdit(int MachID, string duit)
        {
            var stm = "Select * from DuitageGomme where MachineID=@id and Duitage=@d ";
            var duitlist = db.LoadData<Duitages, dynamic>(stm, new { id = MachID, d = duit }, connectionStringName);
            if (duitlist.Count > 0)
                return duitlist[0];
            return null;
        }

        public DuitageGomme GetDuitageGo(int MachID, string duit)
        {
            var stm = "Select * from DuitageGomme where MachineID=@id and UPPER(Duitage) like @d";
            var duitlist =
                db.LoadData<DuitageGomme, dynamic>(stm, new { id = MachID, d = duit.ToUpper() }, connectionStringName);
            if (duitlist.Count > 0)
                return duitlist[0];
            return null;
        }

        public List<Duitages> GetDuitageMachine(Machine machine)
        {
            // string stm = "select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
            // db.LoadData<Duitages, dynamic>(stm, new { id = machine.ID }, connectionStringName);
            using (var conn = new SQLiteConnection(connectionStringName))
            {
                var query =
                    @"select * from Duitages as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";

                var result = conn.Query<Duitages, Machine, Duitages>(query, (duit, mach) =>
                {
                    duit.Machine = mach;
                    return duit;
                }, new { id = machine.ID });

                return result.AsList();
            }
        }

        public List<DuitageGomme> GetDuitageMachineGo(Machine machine)
        {
            // string stm = "select * from DuitageGomme as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";
            // db.LoadData<DuitageGomme, dynamic>(stm, new { id = machine.ID }, connectionStringName);
            using (var conn = new SQLiteConnection(connectionStringName))
            {
                var query =
                    @"select * from DuitageGomme as duit,mMachine as mach where duit.MachineID=mach.ID and mach.ID=@id";

                var result = conn.Query<DuitageGomme, Machine, DuitageGomme>(query, (duit, mach) =>
                {
                    duit.Machine = mach;
                    return duit;
                }, new { id = machine.ID });

                return result.AsList();
            }
        }

        public ModelMachine GetTresseCrochetModelMachine(ModelMachine model)
        {
            var modellist = db.LoadData<ModelMachine, dynamic>(
                "select * from ModelMachine where UPPER(NomModel) like @model and method=@meth",
                new { model = model.NomModel.ToUpper(), meth = model.method }, connectionStringName);
            if (modellist.Count > 0)
                return modellist[0];
            return null;
        }

        public void AddTresseCrochetModelMachine(ModelMachine model)
        {
            var stm = "Insert into ModelMachine(Name,NomModel,method) values(@name,@model,@method)";
            db.SaveData<dynamic>(stm, new { name = model.Name, model = model.NomModel, model.method },
                connectionStringName);
        }

        public void EditMachine(Machine mach)
        {
            var stm = "update mMachine  set Num=@num,DoubleDuitage=@dub,Name=@name,ModelID=@model where ID=@id";
            db.SaveData<dynamic>(stm,
                new { name = mach.Name, num = mach.Num, model = mach.Model.ID, dub = mach.DoubleDuitage, id = mach.ID },
                connectionStringName);
        }

        public void EditDuitage(Duitages duit)
        {
            var stm = "update Duitages  set MachineID=@MachID,Duitage=@duits,Vitesse=@vit where ID=@id";
            db.SaveData<dynamic>(stm,
                new { MachID = duit.Machine.ID, duits = duit.Duitage, vit = duit.Vitesse, id = duit.ID },
                connectionStringName);
        }

        public void EditDuitageGo(DuitageGomme duit)
        {
            var stm = "update DuitageGomme  set MachineID=@MachID,Duitage=@duits where ID=@id";
            db.SaveData<dynamic>(stm,
                new { MachID = duit.Machine.ID, duits = duit.Duitage, vit = duit.Vitesse, id = duit.ID },
                connectionStringName);
        }

        public List<Machine> GetMachines()
        {
            using (var conn = new SQLiteConnection(connectionStringName))
            {
                var query = @"Select * from mMachine as mach,ModelMachine as mMach where mach.ModelID=mMach.ID";

                var result = conn.Query<Machine, ModelMachine, Machine>(query, (mach, mMach) =>
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
                new
                {
                    name = model.Name, nom = model.NomModel, width = model.MaxWidth, nbr = model.NbrBande, id = model.ID
                },
                connectionStringName);
        }

        public void SaveModelCrochetTresseMachineChange(ModelMachine model)
        {
            db.SaveData<dynamic>(
                "Update ModelMachine set Name=@name,NomModel=@nom,method=@meth where ID=@id",
                new { name = model.Name, nom = model.NomModel, id = model.ID, meth = model.method },
                connectionStringName);
        }

        public bool CheckModelDuplicate(ModelMachine model)
        {
            var modellist = db.LoadData<ModelMachine, dynamic>(
                "select * from ModelMachine where NbrBande=@nbr and MaxWidth=@width and NomModel=@NomModel",
                new { nbr = model.NbrBande, width = model.MaxWidth,model.NomModel }, connectionStringName);
            if (modellist.Count > 0)
                return true;
            return false;
        }
        public void DeleteModelMachine(ModelMachine model)
        {
            db.SaveData<dynamic>("Delete from ModelMachine where ID=@id", new { id = model.ID }, connectionStringName);

        }
        public List<Machine> IsModelMachineUsed(ModelMachine model)
        {
          return db.LoadData<Machine,dynamic>("select * from mMachine where ModelID=@id", new { id = model.ID }, connectionStringName);

        }
        public List<Redacteur> GetRedacteurs()
        {
            return db.LoadData<Redacteur, dynamic>("Select * from Redacteur", null, connectionStringName);
        }

        public List<Concepteur> GetConcepteurs()
        {
            return db.LoadData<Concepteur, dynamic>("Select * from Concepteur", null, connectionStringName);
        }
        public void DeleteConcepteur(Concepteur concept)
        {
            db.SaveData<dynamic>("Delete from Concepteur where ID=@id",new { id=concept.ID},connectionStringName);
         
        }
        public void DeleteVerificateur(Verificateur veri)
        {
            db.SaveData<dynamic>("Delete from Verificateur where ID=@id", new { id = veri.ID }, connectionStringName);
          
       }
        public void DeleteMatiere(Matiere mat)
        {
            db.SaveData<dynamic>("Delete from Matiere where ID=@id", new { id = mat.ID }, connectionStringName);

        }
        public void DeleteMachine(Machine mach)
        {
            db.SaveData<dynamic>("Delete from mMachine where ID=@id", new { id = mach.ID }, connectionStringName);

        }
       
        public void AddNewConcepteur(Concepteur concept)
       {
            var stm = "Insert into Concepteur(Name) values(@name)";
            db.SaveData<dynamic>(stm, new { name = concept.Name }, connectionStringName);
      }
        public void AddNewMatiere(Matiere mat)
        {
            var stm = "Insert into Matiere(Ref,Designation,TitrageID,GetCouleurID) values(@Ref,@Designation,@TitrageID,@GetCouleurID)";
            db.SaveData<dynamic>(stm, new { Ref = mat.Ref, Designation = mat.Designation,
                TitrageID = mat.Titrage.ID,
                GetCouleurID = mat.GetCouleur.ID
            }, connectionStringName);
        }
        public void EditMatiere(Matiere NovMatiere)
        {
            string stm = "Update Matiere set Ref=@Ref,@Designation,@TitrageID,@GetCouleurID where ID=@id";
            db.SaveData<dynamic>(stm, new { Ref = NovMatiere.Ref,
                Designation = NovMatiere.Designation,
                TitrageID= NovMatiere.Titrage.ID,
                GetCouleurID= NovMatiere.GetCouleur.ID,
                id= NovMatiere.ID
            }, connectionStringName);

        }
        public void AddNewTypeMatiere(TypeMatiere typmat)
        {
            var stm = "Insert into TypeMatiere(MatiereNom,Codification) values(@MatiereNom,@Codification)";
            db.SaveData<dynamic>(stm, new
            {
                MatiereNom = typmat.MatiereNom,
                Codification = typmat.Codification,
            
            }, connectionStringName);
        }

        public void AddNewTitrage(Titrage tit)
        {
            var stm = "Insert into Titrage(Designation,TypeMatiereID,NumTwist,NumMetric) " +
                "values(@Designation,@TypeMatiereID,@NumTwist,@NumMetric)";
            db.SaveData<dynamic>(stm, new
            {
                Designation = tit.Designation,
                TypeMatiereID = tit.TypeMatiere.ID,
                NumTwist = tit.NumTwist,
                NumMetric = tit.NumMetric,

            }, connectionStringName);
        }
        public void AddNewVerificateur(Verificateur verificateur)
        {
            var stm = "Insert into Verificateur(Name) values(@name)";
            db.SaveData<dynamic>(stm, new { name = verificateur.Name }, connectionStringName);
        }
        public void AddNewCategorie(Catalogue NovCat)
        {
            var stm = "Insert into Catalogue(Designation,parent) values(@Designation,@parent)";
            db.SaveData<dynamic>(stm, new { Designation = NovCat.Designation, parent=NovCat.parent }, connectionStringName);
        }
        public void AddNewClient(Client NovClient)
        {
            var stm = "Insert into Client(Name) values(@name)";
            db.SaveData<dynamic>(stm, new { name = NovClient.Name }, connectionStringName);
        }
        public void AddNewColor(Couleur NovColor)
        {
            var stm = "Insert into Color(Nbr,Name) values(@nbr,@name)";
            db.SaveData<dynamic>(stm, new { name = NovColor.Name,nbr=NovColor.Nbr }, connectionStringName);
        }
        public void AddNewComposant(Composant NovComposant)
        {
            var stm = "Insert into Composant(Name,parent) values(@name,@parent)";
            db.SaveData<dynamic>(stm, new { name = NovComposant.Name,parent=NovComposant.parent }, connectionStringName);
        }
        public List<Verificateur> GetVerificateur()
        {
            return db.LoadData<Verificateur, dynamic>("Select * from Verificateur", null, connectionStringName);
        }
        public List<Couleur> GetCouleurs()
        {
            return db.LoadData<Couleur, dynamic>("Select * from Color", null, connectionStringName);
        }
        public List<Couleur> GetCouleurs(Titrage tit)
      {
            string stm = "select cl.ID,cl.Nbr,cl.Name from Matiere as mat,Titrage as tit,Color as cl where mat.TitrageID=tit.ID and mat.GetCouleurID=cl.ID and tit.ID=@id";
           return db.LoadData<Couleur,dynamic>(stm, new { id = tit.ID }, connectionStringName);

        }
        public List<Matiere> GetMatieres()
        {
            return db.LoadData<Matiere, dynamic>("Select * from Matiere", null, connectionStringName);
        }
        public List<TypeMatiere> GetTypeMatieres()
        {
            return db.LoadData<TypeMatiere, dynamic>("Select * from TypeMatiere", null, connectionStringName);
        }
        public List<Titrage> GetTitrageByTypMat(TypeMatiere typMat)
        {
             return db.LoadData<Titrage, dynamic>("Select Titrage.ID,Titrage.Designation,Titrage.TypeMatiereID,Titrage.NumTwist,Titrage.NumMetric from Titrage,TypeMatiere  where Titrage.TypeMatiereID=TypeMatiere.ID and TypeMatiere.MatiereNom=@MatiereNom", new { MatiereNom = typMat.MatiereNom }, connectionStringName);
            
        }
        public Matiere GetMatiere(string name)
        {
            List<Matiere> matlist = db.LoadData<Matiere, dynamic>("Select * from Matiere  where Ref=@Ref", new { Ref = name }, connectionStringName);
            if (matlist.Count > 0)
            {
                return matlist[0];
            }
            else
            {
                return null;
            }
        }
        public Matiere GetMatiere(Titrage tit, Couleur col)
       {
            string stm = "Select * from Matiere as mat,Titrage as tit,Color as col,TypeMatiere as typmat  " +
                "where GetCouleurID=@cl and TitrageID=@tit and mat.TitrageID=tit.ID and mat.GetCouleurID=col.ID and tit.TypeMatiereID=typmat.ID";
            SQLiteConnection conn = new SQLiteConnection(connectionStringName);
            var matlist= conn.Query<Matiere, Titrage, Couleur, TypeMatiere, Matiere>(stm, (mat, titrage, couleur, typmat)=>
            {
                mat.Titrage = titrage;
                mat.GetCouleur = couleur;
                if(mat.Titrage!=null)
                {
                    mat.Titrage.TypeMatiere = typmat;
                }
                return mat;

            },new { cl = col.ID,tit=tit.ID });

            if (matlist.ToList().Count==0)
                return null;
            return matlist.ToList()[0];

        }
        public TypeMatiere GetTypeMatiereNom(string nom)
        {
            List<TypeMatiere> typmatlist = db.LoadData<TypeMatiere, dynamic>("Select * from TypeMatiere  where MatiereNom=@NomMatiere", new { NomMatiere = nom }, connectionStringName);
            if (typmatlist.Count > 0)
            {
                return typmatlist[0];
            }
            else
            {
                return null;
            }

            //return TypeMatiere.ToList().Find(typmat => typmat.MatiereNom.ToLower().Equals(nom.ToLower()));
        }
        public List<Client> GetClient()
        {
            return db.LoadData<Client, dynamic>("Select * from Client", null, connectionStringName);
        }
        public List<Catalogue> GetCategorie()
        {
            return db.LoadData<Catalogue, dynamic>("Select * from Catalogue", null, connectionStringName);
        }
        public Catalogue GetCategorie(string name)
        {
            List<Catalogue> catlist = db.LoadData<Catalogue, dynamic>("Select * from Catalogue  where Designation=@name", new { name = name }, connectionStringName);
            if (catlist.Count > 0)
            {
                return catlist[0];
            }
            else
            {
                return null;
            }
        }
        public Composant GetComposant(string name)
        {
            List<Composant> complist = db.LoadData<Composant, dynamic>("Select * from Composant  where Name=@name", new { name = name }, connectionStringName);
            if (complist.Count > 0)
            {
                return complist[0];
            }
            else
            {
                return null;
            }
        }
        public Client GetClient(string name)
        {
            List<Client> clientlist = db.LoadData<Client, dynamic>("Select * from Client  where Name=@name", new { name = name }, connectionStringName);
            if (clientlist.Count > 0)
            {
                return clientlist[0];
            }
            else
            {
                return null;
            }
        }
        public Verificateur GetVerificateur(string name)
       {
            List<Verificateur> verlist= db.LoadData<Verificateur, dynamic>("Select * from Verificateur where Name=@name", new { name=name}, connectionStringName);
            if(verlist.Count>0)
            {
                return verlist[0];
            }else
            {
                return null;
            }
        }
        public Concepteur GetConcepteur(string name)
       {
            List<Concepteur> conceptlist= db.LoadData<Concepteur,dynamic>("Select * from Concepteur where Name=@name", new 
            { name=name },connectionStringName);
            if(conceptlist.Count>0)
            {
                return conceptlist[0];
            }
            else
            {
                return null;
            }
      }
        public void EditConcepteur(Concepteur NovConcept)
       {
            string stm = "Update Concepteur set Name=@name where ID=@id";
            db.SaveData<dynamic>(stm, new { name = NovConcept.Name ,id=NovConcept.ID}, connectionStringName);
         
        }
        public void EditClient(Client NovClient)
        {
            string stm = "Update Client set Name=@name where ID=@id";
            db.SaveData<dynamic>(stm, new { name = NovClient.Name, id = NovClient.ID }, connectionStringName);

        }
        public void UpdateTrameX(Enfilage NovEnfilage)
        {
            string stm = "Update Enfilage set TrXposition=@TrameX where ID=@id";
            db.SaveData<dynamic>(stm, new { TrameX = NovEnfilage.TrXposition, id = NovEnfilage.ID }, connectionStringName);
        }
        public void UpdateTrameY(Enfilage NovEnfilage)
        {
            string stm = "Update Enfilage set TrYposition=@TrameY where ID=@id";
            db.SaveData<dynamic>(stm, new { TrameY = NovEnfilage.TrYposition, id = NovEnfilage.ID }, connectionStringName);
        }

        public void EditColor(Couleur NovColor)
        {
            string stm = "Update Color set Name=@name,Nbr=@nbr where ID=@id";
            db.SaveData<dynamic>(stm, new { name = NovColor.Name,nbr=NovColor.Nbr, id = NovColor.ID }, connectionStringName);

        }
        public void EditComposant(Composant NovComposant)
        {
            string stm = "Update Composant set Name=@name where ID=@id";
            db.SaveData<dynamic>(stm, new { name = NovComposant.Name, id = NovComposant.ID }, connectionStringName);

        }
        public void EditCategorie(Catalogue NovCat)
        {
            string stm = "Update Catalogue set Designation=@Designation,parent=@parent where ID=@id";
            db.SaveData<dynamic>(stm, new { NovCat.Designation, id = NovCat.ID, NovCat.parent }, connectionStringName);

        }
        public void EditVerificateur(Verificateur NovVerificateur)
        {
            string stm = "Update Verificateur set Name=@name where ID=@id";

            db.SaveData<dynamic>(stm, new { name = NovVerificateur.Name,id=NovVerificateur.ID }, connectionStringName);
            
        }
        public void DeleteRedacteur(Redacteur red)
        {
            var stm = "Delete from Redacteur where ID=@id";
            db.SaveData<dynamic>(stm, new { id = red.ID }, connectionStringName);
        }
        public void DeleteClient(Client cl)
        {
            var stm = "Delete from Client where ID=@id";
            db.SaveData<dynamic>(stm, new { id = cl.ID }, connectionStringName);
        }
        public void DeleteColor(Couleur color)
        {
            var stm = "Delete from Color where ID=@id";
            db.SaveData<dynamic>(stm, new { id = color.ID }, connectionStringName);
        }
        public void DeleteComposant(Composant comp)
        {
            var stm = "Delete from Composant where ID=@id";
            db.SaveData<dynamic>(stm, new { id = comp.ID }, connectionStringName);
        }
        public void DeleteCategorie(Catalogue cat)
        {
            var stm = "Delete from Catalogue where ID=@id";
            db.SaveData<dynamic>(stm, new { id = cat.ID }, connectionStringName);
        }
        public int AddNewEnfilageMatrix(EnfilageMatrix NewEnfMatrix)
        {
            var stm = "Insert into EnfilageMatrix(ID,x,y,valueID,DentFil,EnfID) values(@id,@x,@y,@val,@df,@enfID)";
            var MatID = Convert.ToInt32(db.SaveData(stm,
                new
                {
                    id = NewEnfMatrix.ID,
                    NewEnfMatrix.x,
                    NewEnfMatrix.y, val = NewEnfMatrix.value.ID,
                    df = NewEnfMatrix.DentFil, enfID = NewEnfMatrix.Enf.ID
                },
                connectionStringName));

            return MatID;
        }
        public int AddEnfilageMultiplier(EnfilageMatrix NewEnfMatrix)
        {
            var stm = "Insert into EnfilageMatrix(ID,x,y,DentFil,EnfID) values(@id,@x,@y,@df,@enfID)";
            var MatID = Convert.ToInt32(db.SaveData(stm,
                new
                {
                    id = NewEnfMatrix.ID,
                    NewEnfMatrix.x,
                    NewEnfMatrix.y,
                    df = NewEnfMatrix.DentFil, enfID = NewEnfMatrix.Enf.ID
                },
                connectionStringName));

            return MatID;
        }
        public void UpdateEnfilageElement(int x, int y, int EnfID, Composition content)
        {
            var stm = "Update EnfilageMatrix set valueID=@val where EnfID=@id and x=@x and y=@y";
            db.SaveData(stm, new { val = content.ID, id = EnfID, x, y }, connectionStringName);
        }

        public void DeleteEnfilageElement(int x, int y, int EnfID)
        {
            var stm = "Delete from EnfilageMatrix where EnfID=@enfid and x=@x and y=@y";

            db.SaveData(stm, new { Enfid = EnfID, x, y }, connectionStringName);
        }
        public void UpdateCompoPoids(Composition compo)
        {
            string stm = "Update Composition set Poids=@poids where ID=@id";
            db.SaveData<dynamic>(stm, new { poids = compo.Poids, id = compo.ID }, connectionStringName);
           
        }
        public void UpdateCompoEnfNbr(Composition compo)
        {
            string stm = "Update Composition set EnfNbrFil=@EnfNbrFil where ID=@id";
            db.SaveData<dynamic>(stm, new { EnfNbrFil = compo.EnfNbrFil, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoNbrFil(Composition compo)
        {
            string stm = "Update Composition set NbrFil=@NbrFil where ID=@id";
            db.SaveData<dynamic>(stm, new { NbrFil = compo.NbrFil, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoNum(Composition compo)
        {
            string stm = "Update Composition set Num=@Num where ID=@id";
            db.SaveData<dynamic>(stm, new { Num = compo.Num, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoNumComposant(Composition compo)
        {
            string stm = "Update Composition set NumComposant=@NumComposant where ID=@id";
            db.SaveData<dynamic>(stm, new { NumComposant = compo.NumComposant, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoTorsion(Composition compo)
        {
            string stm = "Update Composition set Torsion=@Torsion where ID=@id";
            db.SaveData<dynamic>(stm, new { Torsion = compo.Torsion, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoEnfilage(Composition compo)
        {
            string stm = "Update Composition set Enfilage=@Enfilage where ID=@id";
            db.SaveData<dynamic>(stm, new { Enfilage = compo.Enfilage, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoEmb(Composition compo)
        {
            string stm = "Update Composition set Emb=@Emb where ID=@id";
            db.SaveData<dynamic>(stm, new { Emb = compo.Emb, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoObservation(Composition compo)
        {
            string stm = "Update Composition set Observation=@Observation where ID=@id";
            db.SaveData<dynamic>(stm, new { Observation = compo.Observation, id = compo.ID }, connectionStringName);

        }
        public void UpdateCompoMatiere(Composition compo)
        {
            var stm = "Update Composition set GetMatiereID=@mat where ID=@id";
            db.SaveData(stm, new { mat = compo.GetMatiere.ID, id = compo.ID }, connectionStringName);
        }
        public void UpdateCompoComposant(Composition compo)
        {
            var stm = "Update Composition set GetComposantID=@cmp where ID=@id";
            db.SaveData(stm, new { cmp = compo.GetComposant.ID, id = compo.ID }, connectionStringName);
        }
        public List<Composition> GetCompositions(int ProdId, int ProdVersion)
        {
            var stm =
                "Select cmp.ID,cmp.NumComposant,cmp.DebutFil,cmp.Intermittent,cmp.GetComposantID,cmp.Num,cmp.GetMatiereID" +
                ",cmp.NbrFil,cmp.Torsion,cmp.Enfilage,cmp.Emb,cmp.Poids,cmp.Observation,cmp.ProdIDId" +
                ",cmp.EnfNbrFil" +
                " from Composition as cmp,Product as pr where cmp.ProdIDId=pr.Id and pr.Id=@id and pr.Version=@vers";
            return db.LoadData<Composition, dynamic>(stm, new { id = ProdId, vers = ProdVersion },
                connectionStringName);
        }

        public int GetLatestEnfilageMatrixID()
        {
            var stm = "Select * from EnfilageMatrix order by ID Desc LIMIT 1";
            var enflist = db.LoadData<EnfilageMatrix, dynamic>(stm, null, connectionStringName);

            if (enflist.Count > 0)
                return enflist[0].ID + 1;
            return 0;
        }

        public List<FicheTechnique> GetFicheTechniquesEch()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and pr.Version==0 GROUP by pr.FicheId ORDER by pr.Version";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniquesDiver()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr,Client as cl where pr.FicheId=ft.ID and pr.ClientID=cl.ID and cl.Name!='STOCK' GROUP by pr.FicheId ORDER by pr.Version";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniquesStock()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr,Client as cl where pr.FicheId=ft.ID and pr.ClientID=cl.ID and cl.Name='STOCK' GROUP by pr.FicheId ORDER by pr.Version";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniquesEHC()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and ft.ModelFiche>2 GROUP by pr.FicheId ORDER by pr.Version";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniquesNonValid()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and pr.Definite=0 GROUP by pr.FicheId ORDER by pr.Version";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<FicheTechnique> GetFicheTechniquesProd()
        {
            var stm2 =
                "Select Max(pr.Version) as Version,ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive from FicheTechnique as ft,Product as pr where pr.FicheId=ft.ID and pr.Version!=0 GROUP by pr.FicheId";
            var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);


            if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    var stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id order by Version";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }


            return ftlist;
        }
        public List<Produit> GetFusionFicheTechnique(Catalogue categorie,int FicheID)
        {
            string stm = "Select pr.* from FicheTechnique as ft,Product as pr where ft.ID=pr.FicheId and CatalogID=@CatID and ft.ID!=@FTID group by FicheId";

            return db.LoadData<Produit, dynamic>(stm, new { CatID = categorie.ID, FTID= FicheID }, connectionStringName);
        }

        public string GetFichetechniqueCategorie(int id)
        {
            string stm = "Select * from FicheTechnique where  ID=@id";
           var ficheTek= db.LoadData<FicheTechnique, dynamic>(stm, new
            {
                id
            }, connectionStringName);
           if (ficheTek.Count() == 0)
               return "";
           stm = "Select * from Catalogue where ID=@id";
          var categorie= db.LoadData<Catalogue, dynamic>(stm, new { id = ficheTek[0].CatalogID }, connectionStringName);
         return categorie[0].Designation;
        }
        public List<FicheTechnique> GetFicheTechniques()
        {
            
            //var ftlist = db.LoadData<FicheTechnique, dynamic>(stm2, null, connectionStringName);

            using (var conn=new SQLiteConnection(connectionStringName) )
            {
                string stm = "Select ft.ID,ft.Ordre,ft.ModelFiche,ft.CatalogID,ft.IsArchive" +
                             ",cat.ID,cat.Designation,cat.parent" +
                              ",pr.* "+
                             "from FicheTechnique as ft" +
                             " inner join Product as pr on pr.FicheId=ft.ID " +
                             " left join Catalogue as cat  on cat.ID=ft.CatalogID  ";

                var result2= conn.Query<FicheTechnique, Catalogue,Produit, FicheTechnique>(stm, (ft, cat,prod) =>
               {

                   ft.Catalog = cat;
                   ft.Produits = new List<Produit>();
                   ft.Produits.Add(prod);
                   return  ft;
               });
               var ftlist= result2.GroupBy(ft => ft.ID).Select(ftek =>
               {

                   var GroupedFt = ftek.First();
                   GroupedFt.Produits = ftek.Select(ft =>ft.Produits.Single()).OrderBy(pr => pr.Version).ToList();
                   return GroupedFt;
               }).ToList();
               return ftlist;
            }

            /*if (ftlist.Count > 0)
                foreach (var fiche in ftlist)
                {
                    stm =
                        "Select cat.ID,cat.Designation from Catalogue as cat, FicheTechnique as ft where ft.CatalogID=cat.ID and ft.ID=@id";
                    var catlist =
                        db.LoadData<Catalogue, dynamic>(stm, new { id = fiche.ID }, connectionStringName);
                    if (catlist.Count > 0) fiche.Catalog = catlist[0];
                    stm2 = "Select * from Product as pr where pr.FicheId=@id";
                    var prlist = db.LoadData<Produit, dynamic>(stm2, new { id = fiche.ID }, connectionStringName);

                    fiche.Produits = prlist;
                    if (prlist.Count > 0)
                        foreach (var pr in prlist)
                        {
                            var stm3 = "Select * from Composition as comp where comp.ProdIDId=@id";
                            var complist =
                                db.LoadData<Composition, dynamic>(stm3, new { id = pr.Id }, connectionStringName);

                            pr.GetComposition = complist;

                            if (complist.Count > 0)
                                foreach (var compo in complist)
                                {
                                    var stm4 =
                                        "Select * from Composition as compo,Composant as comp where compo.ID=@id and compo.GetComposantID=comp.ID";
                                    var composantList = db.LoadData<Composant, dynamic>(stm4, new { id = compo.ID },
                                        connectionStringName);

                                    if (composantList.Count > 0) compo.GetComposant = composantList[0];
                                }
                        }
                }*/


        }
    }
}