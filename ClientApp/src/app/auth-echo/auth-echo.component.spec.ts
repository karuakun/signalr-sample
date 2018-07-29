import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthEchoComponent } from './auth-echo.component';

describe('AuthEchoComponent', () => {
  let component: AuthEchoComponent;
  let fixture: ComponentFixture<AuthEchoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuthEchoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthEchoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
