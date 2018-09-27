function Stavka(data) {
    this.StavkaNaziv = ko.observable(data.StavkaNaziv);
    this.Kolicina = ko.observable(data.Kolicina);
}

var stavkeMapping = {
    'Stavke': {
        key: function (Stavka) {
            return ko.utils.unwrapObservable(Stavka.Id);
        },
        create: function (options) {
            return new StavkaVM(options.data);
        }
    }
}

var TipKolicineMapping = {
    'TipoviKolicine': {
        key: function (TipKolicine) {
            return ko.utils.unwrapObservable(KolicinaTipId);
        },
        create: function (options) {
            return new StavkaVM(options.data);
        }
    }
}

TipKolicineVM = function (data) {
    var self = this;
    ko.mapping.fromJS(data, TipKolicineMapping, self);
}

StavkaVM = function (data) {

    var self = this;
    ko.mapping.fromJS(data, stavkeMapping, self);
    console.log(self);
};

DispozicijaVM = function (data) {

    var self = this;
    ko.mapping.fromJS(data, {}, self);

    clearForm(self);


    self.save = function () {
        if (validateForm()) {
            $.ajax({
                url: "Dodaj",
                type: "POST",
                data: ko.toJSON(self),
                contentType: "application/json",
                success: function (data) {
                    if (data) {
                        window.location.href="/ModulDispecer/Dispozicija/Dodaj"
                    }   
                    else {
                        $("p.error-msg").text("Dogodila se greška. Unesite validne podatke.");

                    }
                },
                error: function () {

                }



            });
            $("p.error-msg").text("");
        }
        else {
            $("p.error-msg").text("Popunite obavezna polja.");
        }
    }

    self.addStavka = function () {



        if (validateOrderItems()) {



            var TipKolicine = this.KolicinaTipId();
            var stavka = new StavkaVM({ Id: "0", Naziv: this.NazivStavke(), Kolicina: this.Kolicina(), DispozicijaId: 0, KolicinaTipId: TipKolicine });
            self.Stavke.push(stavka);
            // $(".stavke input").val('');
            $("select[name='KolicinaTip']").val("");
            $("input[data-bind='value:Kolicina'").val("");
            $("input[data-bind='value:NazivStavke'").val("");
            var lastTd = $("tr").last();

            appendUnit(lastTd, TipKolicine);

        }

    };

};

function appendUnit(lastTd, TipKolicine) {
    switch (TipKolicine) {
        case 1: lastTd.find(".measurement-unit").append("kg")
            break;
        case 2: lastTd.find(".measurement-unit").append("litar")
            break;
        case 3: lastTd.find(".measurement-unit").append("paleta")
            break;
        default:
            lastTd.find(".measurement-unit").append("")

    }

}

function validateOrderItems() {
    validation = true;
    $(".stavke input").each(function () {
        if (this.value == '') {
            $(this).parent().addClass("has-error");
            validation = false;
        }
        else {
            $(this).parent().removeClass("has-error");
        }

    });

    var option = $("select[name='KolicinaTip']").val();

    if (option == "") {
        validation = false;
        $("select[name='KolicinaTip']").parent().addClass("has-error");
    }
    else {
        $("select[name='KolicinaTip']").parent().removeClass("has-error");
    }

 
    return validation;
}

function validateForm() {
    var validation = true;

    $(".dispozicija.unos .detalji input").each(function () {
        if ($(this).attr("name") === "DodatneInformacije") {
            validation = true;
            return;
        }
       
        if (this.value == '') {
            $(this).parent().addClass("has-error");
            validation = false;
        }
        else {
            $(this).parent().removeClass("has-error");
        }

    });

    $(".dispozicija.unos .detalji select").each(function () {

        if ($(this).val() == "") {
            $(this).parent().addClass("has-error");
        }
    });

    if (Model.Stavke().length === 0) {
        document.getElementById("stavke-error").innerHTML = "Dodajte minimalno jednu stavku.";
        validation = false;
    }
    else{
        document.getElementById("stavke-error").innerHTML = "";
        validation = true;
    }
    return validation;

}

function clearForm() {
    var elements = document.getElementsByTagName("input");
    for (var i = 0; i < elements.length; i++) {
        elements[i].value = "";
    }
}