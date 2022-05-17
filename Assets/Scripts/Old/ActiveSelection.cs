using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    
    public class ActiveSelection : PassiveSelection
    {
        [SerializeField] PassiveSelection _passiveSelectionPrefab;

        List<Character> _goToSelect;
        List<Character> _gosSelected;
        BoxCollider2D boxCollider;
        // public AudioManager audioManager;

        private void Awake()
        {
            _goToSelect = new List<Character>();
            _gosSelected = new List<Character>();
            boxCollider = GetComponent<BoxCollider2D>();
            ActiveEdges(false);

        }

        public void ActiveEdges(bool isActive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(isActive);
            }

            boxCollider.enabled = isActive;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Character character = collision.GetComponent<Character>();
            if (character.IsSelectable)
            {
                _goToSelect.Add(character);
            }

            // if (collision.gameObject.CompareTag("Player"))
                // audioManager = collision.gameObject.GetComponent<AudioManager>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (Input.GetMouseButtonUp(0)) return;
            Character character = collision.GetComponent<Character>();
            if (_goToSelect.Contains(character))
            {
                _goToSelect.Remove(character);
            }
        }

        public void StartSelection(Vector3 firstPoint, Vector3 secondPoint)
        {
            ActiveEdges(true);
            boxCollider.offset = Vector3.zero;
            boxCollider.size = Vector3.zero;
            Draw(firstPoint, secondPoint);

        }

        public void MakeSelection(Vector3 firstPoint, Vector3 secondPoint)
        {
            Draw(firstPoint, secondPoint);
            float xColliderOffset = (firstPoint.x + secondPoint.x) / 2;
            float yColliderOffset = (firstPoint.y + secondPoint.y) / 2;
            boxCollider.offset = new Vector2(xColliderOffset + 1, yColliderOffset - 0.5f);
            boxCollider.size = new Vector2(width, height);
        }

        public void Select()
        {
            Character[] character = _gosSelected.ToArray();
            foreach (Character go in character)
            {
                go.Select(false);
                Destroy(go.transform.GetComponentInChildren<PassiveSelection>().gameObject);
                _gosSelected.Remove(go);
            }

            character = _goToSelect.ToArray();
            foreach (Character go in character)
            {
                go.Select(true);
                PassiveSelection goSelection = Instantiate(_passiveSelectionPrefab, go.transform);
                goSelection.Draw(go.transform);
                _goToSelect.Remove(go);
                _gosSelected.Add(go);
            }

            ActiveEdges(false);
            // if (audioManager != null)
            //     audioManager.PlayAudioSelected(Random.Range(0, audioManager.clipsSelected.Length));
        }
    }
}