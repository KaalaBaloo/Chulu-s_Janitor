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
                new Dialogue("El viaje comienza aquí...", new int[] { 0 }),
                new Dialogue("¿Dónde estoy?", new int[] { 1 })
            },
            new List<Dialogue> // Scene 2
            {
                new Dialogue("Un lugar misterioso...", new int[] { 0 }),
                new Dialogue("Necesito encontrar una salida.", new int[] { 1 }),
                new Dialogue("La aventura te espera.", new int[] { 0 })
            }
        };
    }
    public override List<Dialogue> GetDialogues(int dialogue) => base.GetDialogues(dialogue);

}
