using InyeccionDependencias.Data;
using InyeccionDependencias.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InyeccionDependencias
{

    //  «Inyección de Dependencias» (DI). La inyección de dependencias consiste de manera resumida
    //  en evitar el acoplamiento entre clases utilizando interfaces.Gracias esto, conseguimos que
    //  cada clase tenga una función única, facilitando así el mantenimiento y el soporte de nuestro código.


    // la inyección de dependencias se usa en MVC .net, Angular, etc. Si se está conectando a un servicio externo
    // la url podría cambiar, se usaría inyección de dependencias para que todas las clases que dependen solo
    // se cambie la parte responsable 


    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        GeneradorInformes generador = new GeneradorInformes();
    //        generador.GenerarInforme();
    //    }
    //}



    //// En primer lugar, vamos a analizar los posibles problemas de esta clase:

    //// Baja mantenibilidad por el acoplamiento en entre clases: Es la propia clase la que instancia las que va necesitando,
    //// provocando que cualquier cambio en las clases "auxiliares" pueda cambiar el comportamiento.
    //// Dificultad para realizar pruebas unitarias: En el caso en el que queramos realizar pruebas unitarias, es requisito
    //// que todos los servicios estén activos. Desde el punto de vista de las pruebas, esto es nefasto, podemos no tener
    //// acceso al servidor de correo, o a la base de datos... Con este patrón, es muy difícil testear nuestra clase.

    //internal class GeneradorInformes
    //{
    //    internal void GenerarInforme()
    //    {
    //        using (ApplicationDbContext contexto = new ApplicationDbContext())
    //        {
    //            // Obtenemos los datos desde el contexto
    //            var teachers = contexto.Teachers.Include(x => x.Courses).ThenInclude(x => x.Students).ToList();

    //            // Trabajo de maquetacion
    //            // .......                           

    //            // Enviamos el correo
    //            var emailSender = new EmailSender();
    //            var envioOk = emailSender.Enviar(teachers);
    //            if (!envioOk)
    //            {
    //                // Registramos el fallo en un log
    //            }
    //        }


    //    }
    //}

    //public class EmailSender
    //{
    //    public bool Enviar(List<Teacher> teachers)
    //    {
    //        // Enviar correo
    //        return true;
    //    }
    //}




    // Inyección de Dependencias
    // Vamos a ver que pasa si refactorizamos la clase para inyectar las dependencias, de manera que no tengamos dependencias:


    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //Generamos la clase que hara el trabajo
    //        GeneradorInformes generador = new GeneradorInformes();

    //        //Obtenemos los datos desde el contexto
    //        using (ApplicationDbContext context = new ApplicationDbContext())
    //        {
    //            var teachers = context.Teachers.Include(x => x.Courses).ThenInclude(x => x.Students).ToList();

    //            //Instanciamos la clase de envio de emails
    //            var emailSender = new EmailSender();
    //            generador.GenerarInforme(teachers, emailSender);
    //        }
    //    }
    //}


    //public class GeneradorInformes
    //{
    //    public void GenerarInforme(List<Teacher> teachers, IEmailSender emailSender)
    //    {

    //        // Trabajo de maquetacion
    //        //.......

    //        //Enviamos el correo

    //        var envioOk = emailSender.Enviar(teachers);
    //    }
    //}

    //public interface IEmailSender
    //{
    //    bool Enviar(List<Teacher> teachers);
    //}


    //public class EmailSender : IEmailSender
    //{
    //    public bool Enviar(List<Teacher> teachers)
    //    {
    //        //Enviar correo
    //        return true;
    //    }
    //}

    // En el caso del ejemplo, hemos inyectado las dependencias mediante un parámetro, pero esta solo es una
    // de las maneras.Podemos inyectar dependencias de varias maneras:

    // Inyección de dependencias en parámetros de constructor.
    // Inyección de propiedades.
    // Inyección de dependencias en parámetros de métodos.
    // Vamos a ver más detalladamente cada uno.


    // Inyección de dependencias en parámetros de constructor

    //var emailSender = new EmailSender();
    //--------------------------------------------------------------
    //public class GeneradorInformes
    //{
    //    //Dependencia para el envio de correos
    //    IEmailSender _emailSender;

    //    //Pasamos la dependencia en el construcor
    //    public GeneradorInformes(IEmailSender emailSender)
    //    {
    //        _emailSender = emailSender;
    //    }

    //    public void GenerarInforme(List<Teacher> teachers)
    //    {
    //        //Trabajo de maquetacion
    //        //.......

    //        _emailSender.Enviar(teachers);
    //    }
    //}



    //Inyección de propiedades

    ////Instanciamos la clase de envio de emails
    //var emailSender = new EmailSender();
    //GeneradorInformes generador = new GeneradorInformes();
    //generador.EmailSender = emailSender;
    //generador.GenerarInforme(profesores);
    //-----------------------------------------------------
    //public class GeneradorInformes
    //{
    //    //Propiedad con la dependencia
    //    public IEmailSender EmailSender { get; set; }
    //    public GeneradorInformes()
    //    {
    //    }

    //    public void GenerarInforme(List<Teacher> teachers)
    //    {
    //        //Trabajo de maquetacion
    //        //.......

    //        EmailSender.Enviar(teachers);
    //    }
    //}


    // Inyección de dependencias en parámetros de métodos
    // Es el caso que hemos visto en el primer ejemplo.

    // Contenedor IOC
    // La utilización de contenedores IOC(Inversion Of Control) nos abstraen de la necesidad de
    // generar las clases cada vez.Simplemente es configurar un contenedor el cual nos sirva las
    // dependencias que necesitamos en cada momento.Esto facilita mucho la labor de trabajo al no
    // tener que gestionar nosotros mismos las clases.Existen bastantes sistemas IOC para.NET,
    // pero en este caso, vamos a utilizar "Microsoft.Extensions.DependencyInjection".



    //-----------------------------------------------------------------------------------------------------------------
    public interface IEmailSender
    {
        bool Enviar(List<Teacher> teachers);
    }
    //-----------------------------------------------------------------------------------------------------------------
    public class EmailSender : IEmailSender
    {
        public bool Enviar(List<Teacher> teachers)
        {
            //Enviar correo
            return true;
        }
    }
    //-----------------------------------------------------------------------------------------------------------------





    //-----------------------------------------------------------------------------------------------------------------
    public interface IGeneradorInformes
    {
        void GenerarInforme();
    }
    //-----------------------------------------------------------------------------------------------------------------
    public class GeneradorInformes : IGeneradorInformes
    {
        //Dependencias Inyectadas
        ApplicationDbContext _contexto;
        IEmailSender _emailSender;

        //Inyectamos las dependencias en el constructor
        public GeneradorInformes(ApplicationDbContext contexto, IEmailSender emailSender)
        {
            _emailSender = emailSender;
            _contexto = contexto;
        }


        public void GenerarInforme()
        {
            // acá el include hace una especie de join anidado para hacer más eficiente la consulta y traer
            // los datos solo una vez y no iterando N veces (y consultando N veces)
            var profesores = _contexto.Teachers.Include(x => x.Courses)
                                                .ThenInclude(x => x.Students).ToList();

            //Trabajo de maquetacion
            //.......                           

            //Enviamos el correo
            var envioOK = _emailSender.Enviar(profesores);
            if (!envioOK)
            {
                //Registramos el fallo en un log
            }

        }
    }
    //-----------------------------------------------------------------------------------------------------------------






    //-----------------------------------------------------------------------------------------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            //Generamos la coleccion de servicios (la misma que usa Startup.cs en core)
            IServiceCollection serviceCollection = new ServiceCollection();



            //Registramos el DbContext
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("Persist Security Info=False;User ID=daniel;Password=2222;Initial Catalog=SchoolDB;Data Source=LAPTOP-GQBEO5A5\\SQLEXPRESS2019"));

            //Registramos el EmailSender
            serviceCollection.AddSingleton<IEmailSender, EmailSender>();
            serviceCollection.AddSingleton<IGeneradorInformes, GeneradorInformes>();

            //Construimos el contendor IoC
            //var services = serviceCollection.BuildServiceProvider();



            Injector.GenerarProveedor(serviceCollection);
            //Obtengo clase desde el IOC
            IGeneradorInformes generador = Injector.GetService<IGeneradorInformes>();
            generador.GenerarInforme();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------






    //-----------------------------------------------------------------------------------------------------------------
    public static class Injector
    {
        static IServiceProvider _proveedor;

        public static void GenerarProveedor(IServiceCollection serviceCollection)
        {
            _proveedor = serviceCollection.BuildServiceProvider();
        }

        public static T GetService<T>()
        {
            return _proveedor.GetService<T>();
        }
    }
    //-----------------------------------------------------------------------------------------------------------------


    // Con esto, registraremos todas las dependencias al inicio de nuestro programa, y llamaremos a
    // Injector.GetService<T>() cuando necesitemos utilizar una dependencia.

    // A simple vista, parece que es mucho trabajo y poca recompensa, pero en las siguientes entradas
    // veremos las ventajas que aporta trabajar así cuando hablemos de las pruebas unitarias y del "Moking".
}
