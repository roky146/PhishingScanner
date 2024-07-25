document.addEventListener('DOMContentLoaded', () => {
    const dropArea = document.getElementById('drop-area');
    const fileInput = document.getElementById('fileInput');

    dropArea.addEventListener('dragover', (event) => {
        event.preventDefault();
        dropArea.classList.add('dragging');
    });

    dropArea.addEventListener('dragleave', () => {
        dropArea.classList.remove('dragging');
    });

    dropArea.addEventListener('drop', (event) => {
        event.preventDefault();
        dropArea.classList.remove('dragging');
        fileInput.files = event.dataTransfer.files;
    });
});
