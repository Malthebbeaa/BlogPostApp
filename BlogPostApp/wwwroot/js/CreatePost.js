function selectRating(value) {
    // Fjern "selected" fra alle
    document.querySelectorAll('.rating-box').forEach(el => el.classList.remove('selected'));

    // Marker den valgte
    const selectedBox = document.querySelector(`.rating-box[data-value="${value}"]`);
    if (selectedBox) {
        selectedBox.classList.add('selected');
    }

    // Sæt hidden input
    document.getElementById('ratingInput').value = value;
}