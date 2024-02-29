# Introducción - AccessHome
## Descripción 
Este proyecto fue parte de una propuesta escolar para mejorar la seguridad de ciertas aulas que presentaron robos, para ello se diseño una propuesta de cerradura electronica manejada atraves de una aplicación movil.
La aplicación fue originalmente desarrollada en xamarin (.NET) y se conecta a la cerradura mediante bluetooth, para abrir la cerradura mediante la aplicacion, se envia una señal para abrir o cerrar la cerradura.
La gestión de visitas y usuarios se maneja a través de una base de datos en Firebase, en esta base de datos se almacenan los usuarios y el registro de entradas.

## Instalación y pasos para ejecutar apropiadamente

La aplicación fue originalmente probada en Android 10 - Api 29, usar esta version de android garantiza el buen funcionamiento.

1. Clonar-Descargar el proyecto
2. Abrir la solución
3. Tener listo un emulador con la version android 10 o en su defecto tener conectado un dispositivo fisico para emular.
4. Contar con conexion a internet en el dipositivo.
5. Tener una base de datos en Firebase.
6. Ejecutar el proyecto.

## Estructura del proyecto

La aplicación fue construida bajo el patrón MVVM y sigue el patron de navegacion Shell de Xamarin. 
Cuenta con la siguiente estructura:

1. Models:
   - Firebase: Esta carpeta almacena los modelos de representacion de datos para interactuar con firebase.
    
2. Services: Aquí se almacenan los servicios i.e conexion a db, conexiones a bt.
3. UserControls: Aquí se almacenan controles visuales, que se pueden usar en distintas vistas.
4. ViewModels: Funcionalidades de la interfaz de usuario.
5. Views: Vistas o interfaz de usuario de la aplicación

- App.Xaml : Punto de entrada de la aplicación.
- AppShell.xaml : Estructura general de la aplicación, contiene los elementos del flyout y sus rutas.
- MainPage : Vista, por default se genera aqui y usualmente es la vista principal y raiz de la aplicación.

