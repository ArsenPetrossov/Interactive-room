using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    private Vector3 _centerScreenPosition = new(Screen.width / 2f, Screen.height / 2f);
    private float _distanceRay = 2f;
    private InteractableItem _currentObject;
    private InteractableItem _previusObject;
    private InteractableItem _takenItem;
    [SerializeField] private Transform _holderPosition;
    [SerializeField] private Transform _interactableInventory;
    [SerializeField] private float _force = 2f;

   

    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(_centerScreenPosition);

        FocusObject(ray);

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenAndCloseDoor(ray);
            PickUpAnObject(_currentObject);
        }

        if (Input.GetMouseButtonDown(0))
        {
            ObjectThrow(_takenItem);
        }
    }

    private void FocusObject(Ray ray)
    {
        if (Physics.Raycast(ray, out var hit, _distanceRay))
        {
            HandleObjectFocus(hit.collider.GetComponent<InteractableItem>());
        }
        else
        {
            ClearObjectFocus();
        }
    }

    private void HandleObjectFocus(InteractableItem item)
    {
        if (item != _currentObject && _currentObject != null)
        {
            _previusObject = _currentObject;
            _previusObject.RemoveFocus();
        }

        _currentObject = item;
        _currentObject?.SetFocus();
    }

    private void ClearObjectFocus()
    {
        if (_currentObject != null)
        {
            _previusObject = _currentObject;
            _currentObject.RemoveFocus();
           
        }
    }

    private void OpenAndCloseDoor(Ray ray)
    {
        if (Physics.Raycast(ray, out var hit, _distanceRay))
        {
            Door door = hit.collider.GetComponent<Door>();
            door?.SwitchDoorState();
        }
    }

    private void PickUpAnObject(InteractableItem item)
    {
        if (_takenItem != null && item != _takenItem)
        {
            RemoveObject(_takenItem);
        }

        _takenItem = item;

        if (_takenItem != null)
        {
            _takenItem.SwitchKinematic(true);
            _takenItem.transform.position = _holderPosition.position;
            _takenItem.transform.SetParent(_holderPosition);
        }
    }

    private void RemoveObject(InteractableItem item)
    {
        if (item == null) return;

        item.SwitchKinematic(false);
        _takenItem.transform.SetParent(_interactableInventory);
        _takenItem = null;
    }

    private void ObjectThrow(InteractableItem item)
    {
        if (item == null || _takenItem == null)
            return;

        _takenItem.SwitchKinematic(false);
        _takenItem.transform.SetParent(_interactableInventory);
        _takenItem.Throw(transform.forward, _force);

        _takenItem = null;
    }
}
