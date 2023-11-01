using ClassLibraryGenTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public interface IDB
    {
        /// Este Interfaz se entrga a modo de requisitos mínimos a implementar y probar.
        /// Debési de incluir funcionalidades adicionales

        /// <summary>
        /// Almacena el usuario.
        /// </summary>
        /// <param name="u">Objeto de la clase Usuario que se desea almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/ actualizar la información.</returns>
        bool GuardaUsuario(User u);

        /// <summary>
        /// Lee los datos del usuario que se corresponde con la clave que se recibe como parámetro.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del usuario buscado o NULL si no se localiza.</returns>
        User LeeUsuario(String email);

        /// <summary>
        /// Comprueba si el usuario existe existe y el password se corresponde con la almacenada de forma cifrada.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <param name="password">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna TRUE si los datos de autenticación son válidos.</returns>
        bool ValidaUsuario(string email, string password);

        /// <summary>
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Usuarios.</returns>
        int NumUsuarios();

        /// <summary>
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Usuarios.</returns>
        int NumUsuariosActivos();

        /// <summary>
        /// Almacena el registro de la persona para crear su arbol genealógico.
        /// </summary>
        /// <param name="p">Objeto de la clase persona que se quiere almacenar.</param>
        /// <returns>Verdadero o falso en función de si ha conseguido insertar/ actualizar la información.</returns>
        bool GuardaPersona(Person p);

        /// <summary>
        /// Lee los datos del usuario que se corresponde con la clave que se recibe como parámetro.
        /// </summary>
        /// <param name="email">Cadena con el EMail del usuario que se quiere consultar.</param>
        /// <returns>Retorna el objeto con la infromación del usuario buscado o NULL si no se localiza.</returns>
        Person LeePersona(int idPersona);

        /// <summary>
        /// Retorna el número de usuarios registrados.
        /// </summary>
        /// <returns>Número de Usuarios.</returns>
        int NumPersonas();

        /// <summary>
        /// Nodo raiz que se corresponde con la persona cuyo árbol genealogico estamos buscando.
        /// </summary>
        /// <param name="idPersona">Identifiacdor de la raiz o persona de la que deseamos obtener los ancestros. (0 no hay límite)</param>
        /// <param name="altura">Altura máxima del árbol. (0 no hay límite)</param>
        /// <returns>Retorna un puntero al objeto buscado y a su árbol genealógico.</returns>
        Person Ancestros(int idPersona, int altura);
    }
}
