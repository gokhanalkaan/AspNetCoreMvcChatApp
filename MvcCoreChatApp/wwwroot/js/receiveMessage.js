const messagesContainer = document.getElementById('messages');
connection.on("ReceiveMessage", (usr, message) => {
    let userObject = JSON.parse(usr);
    console.log(`${userObject.Id}: ${message}`);
    console.log(`message length: ${typeof message}`);


    const newItem = document.createElement('p');
    newItem.textContent = message;
    // Listeye yeni öğeyi ekleyin
    messagesContainer.appendChild(newItem);
    // Mesajı ekrana veya belirli bir div'e yerleştirme
});