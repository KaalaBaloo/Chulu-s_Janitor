using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spanish : LanguageData
{
    public Spanish()
    {
        this.uiTexts = new List<string[]>
        {
            new string[]
            {
                "Jugar",
                "Ajustes",
                "Cómic",
                "Créditos",
                "Salir",
            },
            new string[]
            {
                "¿Quieres salir?",
                "Si",
                "No",
            },
            new string[]
            {
                "Ajustes",
                "Pantalla Completa",
                "Silenciar",
                "Música",
                "Efectos de Sonido",
                "Baja Resolución",
                "Media Resolución",
                "Alta Resolución",
                "Ultra Resolución",
                "Inglés",
                "Español",
                "Volver",
            }
        };

        this.levelNames = new string[]
        {
            "1 - ¡CAPITALISMO, ALLÁ VOY!",
            "2 - ¡AY! MIS PIECECITOS…",
            "3 - ¿QUÉ DEMONIOS ES ESO?",
            "4 - NO ME APUNTES CON ESO, QUE PINCHA",
            "5 - ¿Y AHORA SON DOS?",
            "6 - ¡FUERA, BICHO!",
            "7 - ¿TÚ OTRA VEZ?",
            "8 - ¿ENSUCIAR? ¿ESO SE ENSUCIA?",
            "9 - ADIÓS, PALITOS DE PESCADO",
            "10 - EN UN BUEN FREGADO ME METÍ",
            "11 - ¿Y ESTO CUÁNDO SE COBRA?",
            "12 - FRIEGO DE LADO A LADO",
            "13 - AUXILIO, NECESITO ESPACIO PERSONAL",
            "14 - HOY ME SIENTO 007",
            "15 - ¿FOLÍCULO, ERES TÚ?",
            "16 - ESE YA NO ES UN FOLÍCULO...",
            "17 - NO ME PAGAN LO SUFICIENTE PARA ESTO",
            "18 - ¡OFERTA! UNO POR DOS",
            "19 - YO CURRANDO Y LOS DEMÁS DE FIESTA",
            "20 - . . ."
        };

        this.credits = new string[]
        {
            "Desarrollador: Juan Pérez",
            "Artista: Ana Gómez",
            "Música: Carlos López",
            "Agradecimientos especiales a todos los que apoyaron este proyecto."
        };

        this.dialogues = new List<List<Dialogue>>
        {
            new List<Dialogue> // Escena 1 - Nyar - Introducción
            {
                new Dialogue("¡Buenas! Has sido escogido para una tarea de proporciones cósmicas.", "Nyar_02"),
                new Dialogue("¿Pero será este el correcto? Tiene un poco cara de tonto...", "Nyar_01"),
                new Dialogue("No no, que si me pongo tan exquisito me quedaré sin tiempo.", "Nyar_03"),
                new Dialogue("¡Venga despierta que el tiempo vuela!", "Nyar_02")
            },
            new List<Dialogue> // Escena 2 - Cultista - Nivel 1
            {
                new Dialogue("¡Un aplicante! No pensé que nadie se lo tomaría en serio…", "Cultist_01"),
                new Dialogue("Y menos que vendrían...", "Cultist_03"),
                new Dialogue("Tu trabajo es sencillo, pero como el último cayó en una trampa, a otro se lo comieron vivo y el anterior desintegrado en el sitio, me toca explicártelo.", "Cultist_03"),
                new Dialogue("El sitio obviamente está sucio, porque esto de los rituales mancha mucho. Así que te toca el fregado.", "Cultist_02"),
                new Dialogue("Te deseo suerte suerte… Si mueres por lo menos hazlo en un lugar fácil de limpiar", "Cultist_01")
            },
            new List<Dialogue> // Escena 3 - Cultista - Nivel 3
            {
                new Dialogue("Vaya sorpresa, has sobrevivido a tu primer día. Uno más y rompes el récord.", "Cultist_02"),
                new Dialogue("Por cierto, olvidé mencionar que el Ministerio de Derechos de los Trabajadores nos obliga a informarte de que tu seguro médico no cubre los daños causados por: caídas, resbalones, quemaduras, trampas, ataques de monstruos o la muerte, entre otros.", "Cultist_01"),
                new Dialogue("¡Sigue con el buen trabajo!", "Cultist_01")
            },
            new List<Dialogue> // Escena 4 - Cultista - Nivel 6
            {
                new Dialogue("Buen trabajo, no daba un duro por ti pero lo estás haciendo muy bien.", "Cultist_01"),
                new Dialogue("Sobra mencionar que cuanto más te adentres en la mazmorra más resultados de rituales vas a ver.", "Cultist_02"),
                new Dialogue("Algunos son amigables, otros no tantos…", "Cultist_02"),
                new Dialogue("Ya sabes como va el tema, así que sigue con ello", "Cultist_01")
            },
            new List<Dialogue> // Escena 5 - Cultista - Nivel 8
            {
                new Dialogue("Recuerda que no es una fregona mágica y que necesita una lavada de vez en cuando", "Cultist_02"),
                new Dialogue("Usa el cubo por favor, que está quedando más sucio que antes.", "Cultist_03"),
                new Dialogue("Además nos estamos acercando cada vez más a despertar al gran maestro así que los estropicios serán más grandes.", "Cultist_01")
            },
            new List<Dialogue> // Escena 6 - Cultista - Nivel 12
            {
                new Dialogue("¿Sabías que en los antiguos escritos si veías a nuestro gran señor te volvías loco?", "Cultist_02"),
                new Dialogue("…", "Cultist_03"),
                new Dialogue("Espero que no nos pase, que me hace ilusión ver a nuestro gran señor", "Cultist_01")
            },
            new List<Dialogue> // Escena 7 - Cultista - Nivel 15
            {
                new Dialogue("Ey. ¿No habrás visto a Folículo por casualidad?", "Cultist_01"),
                new Dialogue("Me dijo que iba a ser parte del nuevo ritual y llevaba un espejo muy ostentoso con él y dijo algo de una luz sagrada…", "Cultist_02"),
                new Dialogue("Si lo ves por casualidad avísame.", "Cultist_01")
            },
            new List<Dialogue> // Escena 8 - Cultista - Nivel 19
            {
                new Dialogue("Finalmente, el gran día ha llegado. ¿No estás ansioso?", "Cultist_01"),
                new Dialogue("¿Cómo? ¿Qué no te han dicho nada? Era el trabajo de Folículo… Pues me tocará ponerte al corriente otra vez.", "Cultist_03"),
                new Dialogue("Ha llegado el día en el que haremos el ritual para invocar al gran maestro.", "Cultist_02"),
                new Dialogue("Cuando termines con esta sala ven a la siguiente a recibirlo", "Cultist_01")
            },
            new List<Dialogue> // Escena 9 - Cultista - Nivel 20
            {
                new Dialogue("Lamento decirte esto tan solo llegas pero ya nos vamos", "Cultist_01"),
                new Dialogue("El ritual fue un fracaso y no hemos conseguido traer de vuelta al gran maestro", "Cultist_02"),
                new Dialogue("Me voy a casa, te puedes ir cuando termines de limpiar la sala", "Cultist_03")
            },
            new List<Dialogue> // Escena 10 - Cthulhu - Nivel 20
            {
                new Dialogue("Finalmente se me libera de mi profundo letargo", "Cthulhu_01"),
                new Dialogue("Como recompensa por haberme liberado, tu alma será la que abra este festín", "Cthulhu_01"),
                new Dialogue("…", "Cthulhu_01"),
                new Dialogue("¿Me estás escuchando?", "Cthulhu_02")
            },
            new List<Dialogue> // Escena 11 - Cthulhu & Nyar - Ending
            {
                new Dialogue("Un mero mortal como vos no puede retrasar el día del juicio…", "Cthulhu_01"),
                new Dialogue("Me sigue sin escuchar…", "Cthulhu_02"),
                new Dialogue("Da igual, vuestra confianza y necedad no son más que un pequeño tropiezo en mi ilustre plan…", "Cthulhu_01"),
                new Dialogue("¡Venceré al estúpido durmiente y finalmente reinaré en todo el universo!", "Cthulhu_01"),
                new Dialogue("El orgullo que hay que tener para decir palabras tan grandes mientras pierdes contra un mortal con una fregona", "Nyar_02"),
                new Dialogue("¡No! Se supone que tu no tendrías que eSTaR AqUí…", "Cthulhu_01"),
                new Dialogue("Chu!", "Nyar_02"),
                new Dialogue("Quién lo diría, al final si eras el indicado…", "Nyar_02"),
                new Dialogue("…", "Nyar_02"),
                new Dialogue("Bueno… si se ve fríamente todo es todo gracias a mi. Al fin y al cabo fui yo quien te escogió y el que dio la estocada final… Si, todo gracias a mi", "Nyar_01"),
                new Dialogue("Bueno, ya que estás aquí, ¿podrías hacerme un último favor?", "Nyar_02"),
                new Dialogue("Solo fui capaz de debilitar sus poderes, así que aún no estoy seguro de lo que podría pasar", "Nyar_03"),
                new Dialogue("Necesito que te lo lleves a casa y cuides de él para que no cause problemas, ¿vale?", "Nyar_02"),
                new Dialogue("…", "Nyar_01"),
                new Dialogue("No escucho un “no” así que es un “sí” en mi libro", "Nyar_02")
            }
        };
    }

    public override List<Dialogue> GetDialogues(int dialogue) => base.GetDialogues(dialogue);

}
