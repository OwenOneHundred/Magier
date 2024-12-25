using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float cameraSpeed;
    [System.NonSerialized] public bool disableAutomaticCameraMovement = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SnapToTilemapCenter()
    {
        Vector3 tilemapCenter = TilemapManager.tilemapManager.groundTilemap.cellBounds.center;
        transform.position = new Vector3(tilemapCenter.x, tilemapCenter.y, -10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!disableAutomaticCameraMovement)
        {
            MoveToMouseIfFar();
        }
    }

    void MoveToMouseIfFar()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(mousePos, transform.position) > 5.25)
        {
            transform.position += (Vector3)(cameraSpeed * Time.deltaTime * (Vector2)(mousePos - transform.position).normalized);
        }
    }

}
