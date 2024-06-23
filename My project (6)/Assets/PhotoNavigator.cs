using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoNavigator : MonoBehaviour
{
    public Texture2D[] photos; // Array of textures for photos
    public RawImage photoDisplay; // Reference to the RawImage component to display photos

    private int currentPhotoIndex = 0; // Index of the current photo being displayed

    void Start()
    {
        // Display the first photo when the script starts
        DisplayPhoto(currentPhotoIndex);
    }

    // Method to display a photo based on its index
    void DisplayPhoto(int index)
    {
        if (index >= 0 && index < photos.Length)
        {
            photoDisplay.texture = photos[index]; // Set the texture of the RawImage component
            currentPhotoIndex = index; // Update the current photo index
        }
    }

    // Button click event handler for navigating to the next photo
    public void NextPhoto()
    {
        currentPhotoIndex = (currentPhotoIndex + 1) % photos.Length; // Move to the next photo index
        DisplayPhoto(currentPhotoIndex); // Display the next photo
    }

    // Button click event handler for navigating to the previous photo
    public void PreviousPhoto()
    {
        currentPhotoIndex = (currentPhotoIndex - 1 + photos.Length) % photos.Length; // Move to the previous photo index
        DisplayPhoto(currentPhotoIndex); // Display the previous photo
    }
}