/*==============================================================*/
/* Nom de SGBD :  Microsoft SQL Server 2017                     */
/* Date de création :  11/07/2024 20:05:18                      */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ADMIN') and o.name = 'FK_ADMIN_HERITAGE__USER')
alter table ADMIN
   drop constraint FK_ADMIN_HERITAGE__USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ASSOCIATION_2') and o.name = 'FK_ASSOCIAT_ASSOCIATI_PRODUCT')
alter table ASSOCIATION_2
   drop constraint FK_ASSOCIAT_ASSOCIATI_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('ASSOCIATION_2') and o.name = 'FK_ASSOCIAT_ASSOCIATI_CATEGORY')
alter table ASSOCIATION_2
   drop constraint FK_ASSOCIAT_ASSOCIATI_CATEGORY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('CLIENT') and o.name = 'FK_CLIENT_HERITAGE__USER')
alter table CLIENT
   drop constraint FK_CLIENT_HERITAGE__USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMAND') and o.name = 'FK_COMMAND_COMMAND_PRODUCT')
alter table COMMAND
   drop constraint FK_COMMAND_COMMAND_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMAND') and o.name = 'FK_COMMAND_COMMAND2_CLIENT')
alter table COMMAND
   drop constraint FK_COMMAND_COMMAND2_CLIENT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('COMMAND') and o.name = 'FK_COMMAND_COMMAND3_WORKER')
alter table COMMAND
   drop constraint FK_COMMAND_COMMAND3_WORKER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERCAT') and o.name = 'FK_GERERCAT_GERERCAT_ADMIN')
alter table GERERCAT
   drop constraint FK_GERERCAT_GERERCAT_ADMIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERCAT') and o.name = 'FK_GERERCAT_GERERCAT2_CATEGORY')
alter table GERERCAT
   drop constraint FK_GERERCAT_GERERCAT2_CATEGORY
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERPRO') and o.name = 'FK_GERERPRO_GERERPRO_ADMIN')
alter table GERERPRO
   drop constraint FK_GERERPRO_GERERPRO_ADMIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERPRO') and o.name = 'FK_GERERPRO_GERERPRO2_PRODUCT')
alter table GERERPRO
   drop constraint FK_GERERPRO_GERERPRO2_PRODUCT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERWORK') and o.name = 'FK_GERERWOR_GERERWORK_ADMIN')
alter table GERERWORK
   drop constraint FK_GERERWOR_GERERWORK_ADMIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('GERERWORK') and o.name = 'FK_GERERWOR_GERERWORK_WORKER')
alter table GERERWORK
   drop constraint FK_GERERWOR_GERERWORK_WORKER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WORKER') and o.name = 'FK_WORKER_HERITAGE__USER')
alter table WORKER
   drop constraint FK_WORKER_HERITAGE__USER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ADMIN')
            and   type = 'U')
   drop table ADMIN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ASSOCIATION_2')
            and   name  = 'ASSOCIATION_3_FK'
            and   indid > 0
            and   indid < 255)
   drop index ASSOCIATION_2.ASSOCIATION_3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('ASSOCIATION_2')
            and   name  = 'ASSOCIATION_2_FK'
            and   indid > 0
            and   indid < 255)
   drop index ASSOCIATION_2.ASSOCIATION_2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('ASSOCIATION_2')
            and   type = 'U')
   drop table ASSOCIATION_2
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CATEGORY')
            and   type = 'U')
   drop table CATEGORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLIENT')
            and   type = 'U')
   drop table CLIENT
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('COMMAND')
            and   name  = 'COMMAND3_FK'
            and   indid > 0
            and   indid < 255)
   drop index COMMAND.COMMAND3_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('COMMAND')
            and   name  = 'COMMAND2_FK'
            and   indid > 0
            and   indid < 255)
   drop index COMMAND.COMMAND2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('COMMAND')
            and   name  = 'COMMAND_FK'
            and   indid > 0
            and   indid < 255)
   drop index COMMAND.COMMAND_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('COMMAND')
            and   type = 'U')
   drop table COMMAND
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERCAT')
            and   name  = 'GERERCAT2_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERCAT.GERERCAT2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERCAT')
            and   name  = 'GERERCAT_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERCAT.GERERCAT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GERERCAT')
            and   type = 'U')
   drop table GERERCAT
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERPRO')
            and   name  = 'GERERPRO2_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERPRO.GERERPRO2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERPRO')
            and   name  = 'GERERPRO_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERPRO.GERERPRO_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GERERPRO')
            and   type = 'U')
   drop table GERERPRO
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERWORK')
            and   name  = 'GERERWORK2_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERWORK.GERERWORK2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('GERERWORK')
            and   name  = 'GERERWORK_FK'
            and   indid > 0
            and   indid < 255)
   drop index GERERWORK.GERERWORK_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('GERERWORK')
            and   type = 'U')
   drop table GERERWORK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PRODUCT')
            and   type = 'U')
   drop table PRODUCT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('"USER"')
            and   type = 'U')
   drop table "USER"
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WORKER')
            and   type = 'U')
   drop table WORKER
go

/*==============================================================*/
/* Table : ADMIN                                                */
/*==============================================================*/
create table ADMIN (
   IDUSER               numeric(5)           not null,
   NOM                  varchar(10)          not null,
   PRENOM               varchar(10)          not null,
   CIN                  bigint               not null,
   ADRESSMAIL           varchar(15)          null,
   NUMTEL               int                  null,
   DATENAISSANCE        datetime             null,
   constraint PK_ADMIN primary key (IDUSER)
)
go

/*==============================================================*/
/* Table : ASSOCIATION_2                                        */
/*==============================================================*/
create table ASSOCIATION_2 (
   IDPRODUCT            numeric(5)           not null,
   IDCATEGORIE          numeric(5)           not null,
   constraint PK_ASSOCIATION_2 primary key (IDPRODUCT, IDCATEGORIE)
)
go

/*==============================================================*/
/* Index : ASSOCIATION_2_FK                                     */
/*==============================================================*/




create nonclustered index ASSOCIATION_2_FK on ASSOCIATION_2 (IDPRODUCT ASC)
go

/*==============================================================*/
/* Index : ASSOCIATION_3_FK                                     */
/*==============================================================*/




create nonclustered index ASSOCIATION_3_FK on ASSOCIATION_2 (IDCATEGORIE ASC)
go

/*==============================================================*/
/* Table : CATEGORY                                             */
/*==============================================================*/
create table CATEGORY (
   IDCATEGORIE          numeric(5)           identity,
   NOMCATEGORIE         varchar(20)          not null,
   NOMBREPRODUCT        int                  not null,
   DESCRIPTION          text                 null,
   constraint PK_CATEGORY primary key (IDCATEGORIE)
)
go

/*==============================================================*/
/* Table : CLIENT                                               */
/*==============================================================*/
create table CLIENT (
   IDUSER               numeric(5)           not null,
   NOM                  varchar(10)          not null,
   PRENOM               varchar(10)          not null,
   CIN                  bigint               not null,
   ADRESSMAIL           varchar(15)          null,
   NUMTEL               int                  null,
   DATENAISSANCE        datetime             null,
   constraint PK_CLIENT primary key (IDUSER)
)
go

/*==============================================================*/
/* Table : COMMAND                                              */
/*==============================================================*/
create table COMMAND (
   IDPRODUCT            numeric(5)           not null,
   CLI_IDUSER           numeric(5)           not null,
   IDUSER               numeric(5)           not null,
   IDCOMMAND            numeric(5)           identity,
   DATECOMMAND          datetime             not null,
   NOMBREPRODUCT        int                  not null,
   TOTALPRICE           float                not null,
   constraint PK_COMMAND primary key (IDPRODUCT, CLI_IDUSER, IDUSER)
)
go

/*==============================================================*/
/* Index : COMMAND_FK                                           */
/*==============================================================*/




create nonclustered index COMMAND_FK on COMMAND (IDPRODUCT ASC)
go

/*==============================================================*/
/* Index : COMMAND2_FK                                          */
/*==============================================================*/




create nonclustered index COMMAND2_FK on COMMAND (CLI_IDUSER ASC)
go

/*==============================================================*/
/* Index : COMMAND3_FK                                          */
/*==============================================================*/




create nonclustered index COMMAND3_FK on COMMAND (IDUSER ASC)
go

/*==============================================================*/
/* Table : GERERCAT                                             */
/*==============================================================*/
create table GERERCAT (
   IDUSER               numeric(5)           not null,
   IDCATEGORIE          numeric(5)           not null,
   constraint PK_GERERCAT primary key (IDUSER, IDCATEGORIE)
)
go

/*==============================================================*/
/* Index : GERERCAT_FK                                          */
/*==============================================================*/




create nonclustered index GERERCAT_FK on GERERCAT (IDUSER ASC)
go

/*==============================================================*/
/* Index : GERERCAT2_FK                                         */
/*==============================================================*/




create nonclustered index GERERCAT2_FK on GERERCAT (IDCATEGORIE ASC)
go

/*==============================================================*/
/* Table : GERERPRO                                             */
/*==============================================================*/
create table GERERPRO (
   IDUSER               numeric(5)           not null,
   IDPRODUCT            numeric(5)           not null,
   constraint PK_GERERPRO primary key (IDUSER, IDPRODUCT)
)
go

/*==============================================================*/
/* Index : GERERPRO_FK                                          */
/*==============================================================*/




create nonclustered index GERERPRO_FK on GERERPRO (IDUSER ASC)
go

/*==============================================================*/
/* Index : GERERPRO2_FK                                         */
/*==============================================================*/




create nonclustered index GERERPRO2_FK on GERERPRO (IDPRODUCT ASC)
go

/*==============================================================*/
/* Table : GERERWORK                                            */
/*==============================================================*/
create table GERERWORK (
   ADM_IDUSER           numeric(5)           not null,
   IDUSER               numeric(5)           not null,
   constraint PK_GERERWORK primary key (ADM_IDUSER, IDUSER)
)
go

/*==============================================================*/
/* Index : GERERWORK_FK                                         */
/*==============================================================*/




create nonclustered index GERERWORK_FK on GERERWORK (ADM_IDUSER ASC)
go

/*==============================================================*/
/* Index : GERERWORK2_FK                                        */
/*==============================================================*/




create nonclustered index GERERWORK2_FK on GERERWORK (IDUSER ASC)
go

/*==============================================================*/
/* Table : PRODUCT                                              */
/*==============================================================*/
create table PRODUCT (
   IDPRODUCT            numeric(5)           identity,
   NOMPRODUCT           varchar(10)          not null,
   PRICE                int                  not null,
   QUANTITE             int                  not null,
   constraint PK_PRODUCT primary key (IDPRODUCT)
)
go

/*==============================================================*/
/* Table : "USER"                                               */
/*==============================================================*/
create table "USER" (
   IDUSER               numeric(5)           identity,
   NOM                  varchar(10)          not null,
   PRENOM               varchar(10)          not null,
   CIN                  bigint               not null,
   ADRESSMAIL           varchar(15)          null,
   NUMTEL               int                  null,
   DATENAISSANCE        datetime             null,
   constraint PK_USER primary key (IDUSER)
)
go

/*==============================================================*/
/* Table : WORKER                                               */
/*==============================================================*/
create table WORKER (
   IDUSER               numeric(5)           not null,
   NOM                  varchar(10)          not null,
   PRENOM               varchar(10)          not null,
   CIN                  bigint               not null,
   ADRESSMAIL           varchar(15)          null,
   NUMTEL               int                  null,
   DATENAISSANCE        datetime             null,
   constraint PK_WORKER primary key (IDUSER)
)
go

alter table ADMIN
   add constraint FK_ADMIN_HERITAGE__USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

alter table ASSOCIATION_2
   add constraint FK_ASSOCIAT_ASSOCIATI_PRODUCT foreign key (IDPRODUCT)
      references PRODUCT (IDPRODUCT)
go

alter table ASSOCIATION_2
   add constraint FK_ASSOCIAT_ASSOCIATI_CATEGORY foreign key (IDCATEGORIE)
      references CATEGORY (IDCATEGORIE)
go

alter table CLIENT
   add constraint FK_CLIENT_HERITAGE__USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

alter table COMMAND
   add constraint FK_COMMAND_COMMAND_PRODUCT foreign key (IDPRODUCT)
      references PRODUCT (IDPRODUCT)
go

alter table COMMAND
   add constraint FK_COMMAND_COMMAND2_CLIENT foreign key (CLI_IDUSER)
      references CLIENT (IDUSER)
go

alter table COMMAND
   add constraint FK_COMMAND_COMMAND3_WORKER foreign key (IDUSER)
      references WORKER (IDUSER)
go

alter table GERERCAT
   add constraint FK_GERERCAT_GERERCAT_ADMIN foreign key (IDUSER)
      references ADMIN (IDUSER)
go

alter table GERERCAT
   add constraint FK_GERERCAT_GERERCAT2_CATEGORY foreign key (IDCATEGORIE)
      references CATEGORY (IDCATEGORIE)
go

alter table GERERPRO
   add constraint FK_GERERPRO_GERERPRO_ADMIN foreign key (IDUSER)
      references ADMIN (IDUSER)
go

alter table GERERPRO
   add constraint FK_GERERPRO_GERERPRO2_PRODUCT foreign key (IDPRODUCT)
      references PRODUCT (IDPRODUCT)
go

alter table GERERWORK
   add constraint FK_GERERWOR_GERERWORK_ADMIN foreign key (ADM_IDUSER)
      references ADMIN (IDUSER)
go

alter table GERERWORK
   add constraint FK_GERERWOR_GERERWORK_WORKER foreign key (IDUSER)
      references WORKER (IDUSER)
go

alter table WORKER
   add constraint FK_WORKER_HERITAGE__USER foreign key (IDUSER)
      references "USER" (IDUSER)
go

