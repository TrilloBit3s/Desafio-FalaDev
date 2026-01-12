/*

 Bem vindo ao FalaDevs!

 Seu codigo começa aqui, edite ele para poder usar a chave para abrir a porta!

 Depois Grave um breve video e envie seu projeto para o GitHub, e no grupo Unity Brasil no Facebook,
 poste o link do GitHub com seu projeto e o video feito por você.

 Boa sorte!

 Link GitHub:  https://github.com/RafaelReis891/FalaDevs

*/

using UnityEngine;
using UnityEngine.UI;

public class Locked_Door : MonoBehaviour
{
	public bool locked = true;
	public Text txt_DoorState;
	public bool haveKey = false;

	[Header("Door")]
	public Transform Door_To_Open;
    private bool canOpenDoor;

	private void Update()
	{
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
		{
			if (hit.collider.CompareTag("Key"))
			{
				CancelInvoke("DesactivateLabel");
				txt_DoorState.text = "<b><Color=Lime>Chave!</Color> \n" +
				"<Color=Magenta>Aperte 'E' para pegar a chave.</Color></b>";

				if (Input.GetKeyDown("e"))
				{
					Destroy(hit.transform.gameObject);
					haveKey = true;

					txt_DoorState.text = "<b><Color=Lime>Parabéns, você encontrou a chave.</Color> \n" +
						"<Color=Magenta>Vá até a porta para tentar abri-la.</Color></b>";
					
				}
                Invoke("DesactivateLabel", 3);
            }
		}

        if(canOpenDoor)
        {
            Door_To_Open.Rotate(0, 0, Time.deltaTime * -50f, Space.Self);

            if (Door_To_Open.localEulerAngles.y - 360 < -90.0f)
            {
                Door_To_Open.transform.eulerAngles = new Vector3(-90.0f, 0, -90.0f);
                canOpenDoor = false;
            }
        }
	}

	void DesactivateLabel()
	{
		txt_DoorState.text = "";
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.CompareTag("Door"))
		{
            Invoke("DesactivateLabel", 3);

            if(haveKey)
            {
                locked = false;
            }

            if (locked)
			{
				txt_DoorState.text = "<b><Color=Lime>A porta está trancada!</Color> \n" +
				"<Color=Magenta>Desenvolva um script capaz de destrancar ela juntamente com uma chave, a chave está em cima de uma mesa em um dos cômodos. Pegue ela " +
				"e crie a lógica para destrancar a porta com esta chave.</Color></b>";
			}
			else
			{
				txt_DoorState.text = "<b><Color=Lime>A porta foi destrancada!</Color> \n" +
				"<Color=Magenta>Parabéns!, está livre agora.</Color></b>";
                canOpenDoor = true;
            }
		}
	}

	private void OnTriggerExit(Collider other)
	{
		txt_DoorState.text = "";
	}
}
