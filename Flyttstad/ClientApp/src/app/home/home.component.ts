import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  form: FormGroup;
  http: HttpClient;
  baseUrl: string;
  selectedItems = [];
  public dropdownSettings = {};
  cities: City[];
  options: Options[];
  offer: Offer = {} as Offer;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, fb: FormBuilder) {
    this.baseUrl = baseUrl;
    this.http = http;
    http.get<Data>(baseUrl + 'home').subscribe(result => {
      this.cities = result.cities;
    }, error => console.error(error));

    this.form = fb.group({
      cityId: ['', Validators.required],
      squareMeter: ['', [Validators.required, Validators.min(1)]],
      options: ['']
    });
  }

  ngOnInit() {
    this.dropdownSettings = {
      singleSelection: false,
      enableCheckAll: true,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Välj alla',
      unSelectAllText: 'Välj bort alla',
      noDataAvailablePlaceholderText: 'Ingen data tillgänglig'
    };
  }

  onChange(event) {
    const cityId = event.target.value;
    this.http.get<Options[]>(this.baseUrl + 'home/getOptions/' + cityId).subscribe(result => {
      this.options = result;
    }, error => console.error(error));
  }

  onSubmit() {
    if (this.form.invalid) {
      alert('Lägg till rätt värden i formuläret');
      return;
    }

    let data = this.form.getRawValue();
    const body = {
      cityId: data.cityId,
      squareMeter: data.squareMeter,
      options: data.options || null
    }

    this.http.post<Offer>(this.baseUrl + 'home', body).subscribe(result => {
      this.offer = result;
    }, error => console.error(error));
  }
}

interface Data {
  cities: City[];
}

interface City {
  id: number;
  name: string;
  squareMeterPrice: number;
  optionals: Options[];
}

interface Options {
  id: number;
  name: string;
  price: number;
}

interface Offer {
  city: string;
  squareMeter: number;
  price: number;
  options: string[];
}
