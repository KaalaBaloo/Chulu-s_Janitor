using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spanish : DialogueText
{
    public Spanish()
    {
        this.uiTexts = new string[]
        {
            "Iniciar Juego",
            "Continuar",
            "Opciones",
            "Salir",
            "Cargar Juego",
            "Guardar Juego",
            "Volumen",
            "Brillo",
            "Idioma",
            "Controles"
        };

        this.levelNames = new string[]
        {
            "Nivel 1",
            "Nivel 2",
            "Nivel 3",
            "Nivel 4",
            "Nivel 5"
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
            new List<Dialogue> // Scene 1
            {
                new Dialogue("El viaje comienza aquí...", "Nyar_02"),
                new Dialogue("¿Dónde estoy?", "Nyar_02")
            },
            new List<Dialogue> // Scene 2
            {
                new Dialogue("Un lugar misterioso...", "Nyar_02"),
                new Dialogue("Necesito encontrar una salida.", "Nyar_02"),
                new Dialogue("La aventura te espera.", "Nyar_02")
            }
        };
    }
    public override List<Dialogue> GetDialogues(int dialogue) => base.GetDialogues(dialogue);

}
