//-----------------------------------------------------------------------
// <copyright file="ObjectController.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Controls target objects behaviour.
/// </summary>
public class ObjectController : MonoBehaviour
{
    /// <summary>
    /// The material to use when this object is inactive (not being gazed at).
    /// </summary>
    public Material InactiveMaterial;

    /// <summary>
    /// The material to use when this object is active (gazed at).
    /// </summary>
    public Material GazedAtMaterial;

    // The objects are about 1 meter in radius, so the min/max target distance are
    // set so that the objects are always within the room (which is about 5 meters
    // across).
    private const float _minObjectDistance = 17f;
    private const float _maxObjectDistance = 17f;
    private const float _minObjectHeight = 0.5f;
    private const float _maxObjectHeight = 3.5f;

    private Renderer _myRenderer;
    private Vector3 _startingPosition;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    public void Start()
{
    // Obtener la posición inicial de la cámara del jugador (normalmente la cámara principal)
    Camera mainCamera = Camera.main;
    Transform cameraTransform = mainCamera.transform;

    // Definir una distancia en frente del jugador (puedes ajustarlo según lo que necesites)
    float distanciaFrenteJugador = 10f;  // Distancia en metros frente al jugador (ajústalo a tu preferencia)

    // Colocar el objeto a una posición en la dirección que está mirando la cámara
    Vector3 posicionInicial = cameraTransform.position + cameraTransform.forward * distanciaFrenteJugador;

    // Establecer la nueva posición del objeto (también puedes ajustarla según el eje Y)
    transform.parent.position = posicionInicial;

    // Puedes incluir un ajuste para mantener el objeto dentro del rango en el eje Y si es necesario
    transform.parent.position = new Vector3(transform.parent.position.x, 0, transform.parent.position.z);

    // Inicializar el renderer y configurar el material al estado inicial
    _myRenderer = GetComponent<Renderer>();
    SetMaterial(false);
}


    /// <summary>
    /// Teleports this instance randomly when triggered by a pointer click.
    /// </summary>
 public void TeleportRandomly()
{
    // Establecer una posición fija en z = 17, y un rango limitado en X e Y
    float xOffset = Random.Range(-5f, 5f);  // Rango en el eje X
    float yOffset = Random.Range(-2f, 2f);  // Rango en el eje Y

    // Definir la posición estática en Z = 17
    float zPosition = 17f;

    // Nueva posición con offset aleatorio dentro de un área
    Vector3 newPos = new Vector3(xOffset, yOffset, zPosition);

    // Coloca el objeto en la nueva posición (teletransporta al objeto)
    transform.parent.position = newPos;

    // (Opcional) Restaurar el material al estado inicial si es necesario
    SetMaterial(false);
}



    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        SetMaterial(true);
        PuntuacionManager.Instance.AgregarPuntos(10);
        TeleportRandomly();
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen
    /// is touched.
    /// </summary>
    public void OnPointerClick()
    {
        TeleportRandomly();
    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
