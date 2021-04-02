$(document).ready(function() {
    envoiePanier (0,0);
});

function ClicCategory(id) {
    var url = 'Produit?idCategory=' + id;
    $.getJSON(url, function (data) {
        const elemParent = document.getElementById("product");
        destroy(elemParent);
        ContructEntete();
        for (var i = 0; i < data.length; i++) {
            if ( !data[i].Deleted && data[i].VisibleIndividually ) {
                data[i].OrderMaximumQuantity = 5;
                ConstructArticle(data[i].Id,data[i].Name, data[i].FullDescription, data[i].Price, data[i].StockQuantity,data[i].OrderMinimumQuantity,data[i].OrderMaximumQuantity,i);
            }
        };
    })
        .fail(function () {

        });
};

function ContructEntete() {
    const elemParent = document.getElementById("product");
    var div = document.createElement("div");
    div.classList.add('row');
    div.classList.add('bg-primary');
    div.classList.add('text-light');

    var elem = document.createElement("div");
    elem.innerHTML = '<h2>Description du Produit</h2>';
    elem.classList.add('col-12');
    elem.classList.add('text-center');
    elem.classList.add('text-lg-left');
    div.appendChild(elem);
    elemParent.appendChild(div);
};

function ConstructArticle(id,name, fullDescription, price, stock,minVendre,maxVendre, couleur) {
    const elemParent = document.getElementById("product");
    const elemSaut = document.createElement("div");
    elemSaut.innerHTML ="<hr/>";

    elemParent.appendChild(elemSaut);

    var row = document.createElement("div");
    row.classList.add('row');
    if ( 0!=(couleur % 2) ) {
        row.classList.add('bg-light');
    };
    
    var col8 = document.createElement("div");
    col8.classList.add('col-12');
    col8.classList.add('col-md-8');
    var media = document.createElement("div");
    media.classList.add('media');
    var img = document.createElement("img");
    img.classList.add('align-self-center');
    img.classList.add('mr-3');
    //img.classList.add('img-thumbnail');
    img.style="width:60px; height:60px";
    img.src='HandlerImage?id='+id;
    img.alt='Image du produit';
    media.appendChild(img);     //--

    var mediabody = document.createElement("div");
    mediabody.classList.add('media-body');
    var h5 = document.createElement("h5");
    h5.classList.add('mt-0');
    h5.innerText=name;
    mediabody.appendChild(h5);
    var p_col8 = document.createElement("p");
    p_col8.innerHTML=fullDescription;
    mediabody.appendChild(p_col8);      //--
    media.appendChild(mediabody);       //--
    col8.appendChild(media);        //--
    row.appendChild(col8);          //--**
    

    var col4 = document.createElement("div");
    col4.classList.add('col-12');
    col4.classList.add('col-md-4');
    var container = document.createElement("div");
    container.classList.add('container');
    container.classList.add('border');
    container.classList.add('border-warning');
    var rowcontainer = document.createElement("div");
    rowcontainer.classList.add('row');
    var col12rowcontainer = document.createElement("div");
    col12rowcontainer.classList.add('col-12');
    var pPrice = document.createElement("p");
    pPrice.innerText = price+' €';
    pPrice.classList.add('text-center');
    pPrice.classList.add('text-danger');
    pPrice.classList.add('font-weight-bold');
    col12rowcontainer.appendChild(pPrice);         //--
    var pStock = document.createElement("p");
    if ( 0!==parseInt(stock) ) {
        pStock.innerText="En stock";
        pStock.classList.add("text-success");
    }
    else {
        pStock.innerText="En rupture";
        pStock.classList.add("text-danger");
    };
    col12rowcontainer.appendChild(pStock);      //--
    var pQuantite = document.createElement("p");
    pQuantite.innerText = "Quantité : ";
    var selectQ = document.createElement("select");
    selectQ.setAttribute('id',id);
    selectQ.setAttribute('name',id);

    for (var i = minVendre; i< maxVendre;i++) {
        var option = new Option(i,i);
        selectQ.options.add(option);    //--
    };
    pQuantite.appendChild(selectQ);
    col12rowcontainer.appendChild(pQuantite);   //--

    var pButton = document.createElement("p");
    pButton.classList.add('text-center');
    var button= document.createElement("input");
    button.setAttribute('type','button');
    button.setAttribute('value','Ajouter au panier');
    button.setAttribute('onclick','envoiePanier ('+id+',0)');
    if ( 0===stock ) 
    {
        button.setAttribute('disabled','disabled');
    };
    pButton.appendChild(button);        //--
    col12rowcontainer.appendChild(pButton);     //--
    rowcontainer.appendChild(col12rowcontainer);        //--
    container.appendChild(rowcontainer);        //--
    col4.appendChild(container);      //--  
    row.appendChild(col4);      //--**


    elemParent.appendChild(row);
};

function envoiePanier (id,code) {
    var url = 'Panier?idProduct=';
    var elem ;
    var qte;
    if ( 0 ==id ) {
        qte = 0
    }
    else{
        if ( 0 == code ) {
            elem = document.getElementById(id);
            qte = elem.options[elem.selectedIndex].value;
        }
        else{
            elem = document.getElementById('input'+id);
            qte = elem.value;
        }
    }
    
    url += id + '&qte=' + qte + '&code=' + code;
    //console.log(url);
    $.getJSON(url, function (data) {
        const panier = document.getElementById("panierTotal");
        const qte = document.getElementById("qte");
        qte.innerText = data.QteProduits;

        destroy (panier);
        panierData (data);

    })
    .done(function() { 
    //    console.log('traitement terminé'); 
    })
    .always(function() 
    {
        //console.log('traiement toujours');
    })
    .fail(function () {
        console.log('probleme');
    });
};

function destroy ( elemParent ) {
    while(elemParent.firstChild) {
        elemParent.removeChild(elemParent.lastChild);
    }
};


function panierData (data) {
    const panier = document.getElementById("panierTotal");

    for ( var i =0; i< data['Lignes'].length; i++ ) {

        var divRow = document.createElement("div");
        divRow.classList.add('row');
        if ( 1!=i%2 ) {
            divRow.classList.add('bg-white');
        }else {
            divRow.classList.add('bg-light');
        };
        
        var divColImg = document.createElement("div");
        divColImg.classList.add('col-2');
        divColImg.classList.add('p-0');
        var img =document.createElement("img");
        //img.src = 'WebFormImage?id='+data['Lignes'][i].IdProduit;
        img.src = 'HandlerImage?id='+data['Lignes'][i].IdProduit;
        img.classList.add('rounded');
        img.classList.add('img-thumbnail');
        img.alt='Image du produit';
        divColImg.appendChild(img);
        divRow.appendChild(divColImg);

        var divColName = document.createElement("div");
        divColName.classList.add('col-4');
        divColName.classList.add('px-1');
        divColName.classList.add('text-primary');
        divColName.innerText= data['Lignes'][i].NomProduit;
        divRow.appendChild(divColName);

        var divColQte = document.createElement("div");
        var forDiv = document.createElement("div");
        var input = document.createElement("input");
        input.setAttribute('type','text');
        input.setAttribute('value',String(data['Lignes'][i].Quantite));
        input.setAttribute('id','input'+data['Lignes'][i].IdProduit);
        input.setAttribute('size',2);
        input.setAttribute('onchange','envoiePanier ('+data['Lignes'][i].IdProduit+',1)');
        input.classList.add('px-1');
        input.classList.add('form-control');
        forDiv.classList.add('form-group');
        divColQte.classList.add('col-1');
        divColQte.classList.add('p-0');
        
        forDiv.appendChild(input);
        divColQte.appendChild(forDiv);
        divRow.appendChild(divColQte);

        var divprice = document.createElement("div");
        divprice.classList.add('col-2');
        divprice.classList.add('p-0');
        divprice.classList.add('text-center');
        divprice.innerText = data['Lignes'][i].Prix+'€';
        divRow.appendChild(divprice);

        var divTotalLigne = document.createElement("div");
        divTotalLigne.classList.add('col-2');
        divTotalLigne.classList.add('p-0');
        divTotalLigne.innerText = data['Lignes'][i].TotalLigne+'€';
        divRow.appendChild(divTotalLigne);

        var divSupp = document.createElement("div");
        var p = document.createElement("p");
        divSupp.classList.add('col-1');
        p.innerHTML = '<i class="fas fa-trash-alt"></i>';
        p.setAttribute('onclick','envoiePanier ('+data['Lignes'][i].IdProduit+',2)')
        divSupp.appendChild(p);
        divRow.appendChild(divSupp);
        
        panier.appendChild(divRow);
    };
    
    var div = document.createElement("div");
    div.classList.add('font-weight-bold');
    div.classList.add('text-danger');
    div.innerText='Total du Panier : '+data.TotalPanier+' €';
    panier.appendChild(div);
    
};